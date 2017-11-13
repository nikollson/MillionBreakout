
using System;
using System.Linq;
using UnityEngine;

namespace Stool.MilllionBullets.Buffer
{
    class EmptyIndexQueue
    {
        public int RestQueue { get; private set; }

        private ComputeShader _emptyComputeShader;
        private ComputeBuffer _emptyIndexQueue;
        private ComputeBuffer _statesBuffer;
        private ComputeBuffer _inoutBuffer;
        private ComputeBuffer _pushQueueCounter;
        private int[] _inoutReciveArray;
        private int[] _counterRecieveArray;

        public EmptyIndexQueue(ComputeShader emptyComputeShader, ComputeBuffer statesBuffer, int length)
        {
            _emptyComputeShader = emptyComputeShader;
            _statesBuffer = statesBuffer;
            _emptyIndexQueue = new ComputeBuffer(length, sizeof(int), ComputeBufferType.Append);
            _inoutBuffer = new ComputeBuffer(length, sizeof(int));
            _pushQueueCounter = new ComputeBuffer(8*1*1, sizeof(int), ComputeBufferType.Counter);

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
            int kernel = _emptyComputeShader.FindKernel("PopIndices");
            _emptyComputeShader.SetInt("N",num);
            _emptyComputeShader.SetBuffer(kernel, "InoutBuffer", _inoutBuffer);
            _emptyComputeShader.SetBuffer(kernel, "ConsumeQueue", _emptyIndexQueue);
            _emptyComputeShader.Dispatch(kernel, (num - 1) / 8 + 1, 1, 1);

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
            int kernel = _emptyComputeShader.FindKernel("Update");
            _emptyComputeShader.SetBuffer(kernel,"PushQueueCounter", _pushQueueCounter);
            _emptyComputeShader.SetBuffer(kernel, "States",_statesBuffer);
            _emptyComputeShader.SetBuffer(kernel, "AppendQueue", _emptyIndexQueue);
            _emptyComputeShader.Dispatch(kernel, (_emptyIndexQueue.count - 1) / 8 + 1, 1, 1);

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
