
using System;
using System.Linq;
using UnityEngine;

namespace Stool.MilllionBullets.Buffer
{
    class EmptyIndexQueue
    {
        public int RestQueue { get; private set; }
        public int Length { get; private set; }

        private ComputeShader _basicComputeShader;
        private ComputeBuffer _emptyIndexQueue;
        private ComputeBuffer _statesBuffer;
        private ComputeBuffer _inoutBuffer;
        private ComputeBuffer _pushQueueCounter;
        private int[] _inoutReciveArray;
        private int[] _counterRecieveArray;

        private readonly int ThreadNum = 8;

        public EmptyIndexQueue(ComputeShader basicComputeShader, ComputeBuffer statesBuffer, int length)
        {
            Length = length;
            _basicComputeShader = basicComputeShader;
            _statesBuffer = statesBuffer;
            _emptyIndexQueue = new ComputeBuffer(Length, sizeof(int), ComputeBufferType.Append);
            _inoutBuffer = new ComputeBuffer(Length, sizeof(int));
            _pushQueueCounter = new ComputeBuffer(ThreadNum, sizeof(int));

            _inoutReciveArray = new int[_inoutBuffer.count];
            _counterRecieveArray = new int[_pushQueueCounter.count];
            Reset();
        }

        public void Reset()
        {
            RestQueue = _emptyIndexQueue.count;
            var array = new int[_emptyIndexQueue.count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            _emptyIndexQueue.SetData(array);
            _emptyIndexQueue.SetCounterValue((uint)array.Length);
        }

        public int[] PopEmptyIndices(int num)
        {
            RestQueue = Math.Max(0, RestQueue - num);
            int kernel = _basicComputeShader.FindKernel("PopIndices");
            _basicComputeShader.SetInt("N",num);
            _basicComputeShader.SetBuffer(kernel, "InoutBuffer", _inoutBuffer);
            _basicComputeShader.SetBuffer(kernel, "ConsumeQueue", _emptyIndexQueue);
            _basicComputeShader.Dispatch(kernel, (num - 1) / ThreadNum + 1, 1, 1);

            _inoutBuffer.GetData(_inoutReciveArray);
            var ret = new int[num];
            for (int i = 0; i < num; i++)
            {
                ret[i] = _inoutReciveArray[i];
            }
            return ret;
        }

        public void Update()
        {
            int kernel = _basicComputeShader.FindKernel("Update");
            _basicComputeShader.SetBuffer(kernel,"PushQueueCounter", _pushQueueCounter);
            _basicComputeShader.SetBuffer(kernel, "States",_statesBuffer);
            _basicComputeShader.SetBuffer(kernel, "AppendQueue", _emptyIndexQueue);
            _basicComputeShader.Dispatch(kernel, (_emptyIndexQueue.count - 1) / ThreadNum + 1, 1, 1);

            int sumPre = _counterRecieveArray.Sum(x => x);
            _pushQueueCounter.GetData(_counterRecieveArray);
            int sumAfter = _counterRecieveArray.Sum(x => x);
            int push = sumAfter - sumPre;

            RestQueue += push;
        }

        public void Release()
        {
            _emptyIndexQueue.Release();
            _inoutBuffer.Release();
            _pushQueueCounter.Release();
        }
    }
}
