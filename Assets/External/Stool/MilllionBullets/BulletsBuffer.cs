
using System.Runtime.InteropServices;
using Stool.CodingSupport;
using Stool.MilllionBullets.Buffer;
using UnityEngine;

namespace Stool.MilllionBullets
{
    class BulletsBuffer<TOption> where TOption:struct 
    {
        public int Length { get; private set; }
        public int CopyBufferLength { get; private set; }
        private BufferFunctionsBase<TOption> _functions;

        private EmptyIndexQueue _emptyIndexQueue;
        private ComputeBuffer _optionsBuffer;
        private ComputeBuffer _statesBuffer;
        private ComputeBuffer _indicesBuffer;
        private ComputeBuffer _optionsCopyBuffer;
        private ComputeBuffer _statesCopyBuffer;
        private ComputeShader _emptyComputeShader;

        public BulletsBuffer(BufferFunctionsBase<TOption> functions, ComputeShader emptyComputeShader)
        {
            Length = functions.GetLength();
            CopyBufferLength = functions.GetCopyBufferLength();
            _functions = functions;
            _emptyComputeShader = emptyComputeShader;

            _statesBuffer = new ComputeBuffer(Length, Marshal.SizeOf(typeof(BulletState)));
            _optionsBuffer = new ComputeBuffer(Length, Marshal.SizeOf(typeof(TOption)));
            _indicesBuffer = new ComputeBuffer(Length, sizeof(int));
            _statesCopyBuffer = new ComputeBuffer(CopyBufferLength, Marshal.SizeOf(typeof(BulletState)));
            _optionsCopyBuffer = new ComputeBuffer(CopyBufferLength, Marshal.SizeOf(typeof(TOption)));
            _emptyIndexQueue = new EmptyIndexQueue(emptyComputeShader, _statesBuffer, Length);
        }

        public void AddBullets(BulletState[] states, TOption[] options)
        {
            var indices = _emptyIndexQueue.PopEmptyIndices(states.Length);
            if (states.Length <= CopyBufferLength)
            {
                AddBulletsAt(indices, states, options);
            }
            else
            {
                var splitIndices = indices.SplitByLength(CopyBufferLength);
                var splitStates = states.SplitByLength(CopyBufferLength);
                var splitOptions = options.SplitByLength(CopyBufferLength);
                for (int i = 0; i < splitStates.Count; i++)
                {
                    AddBulletsAt(splitIndices[i], splitStates[i], splitOptions[i]);
                }
            }
        }

        private void AddBulletsAt(int[] indices, BulletState[] states, TOption[] options)
        {
            _indicesBuffer.SetData(indices);
            _statesCopyBuffer.SetData(states);
            _optionsCopyBuffer.SetData(options);

            _functions.AddOptions(_optionsBuffer, indices.Length, _indicesBuffer, _optionsCopyBuffer);
            AddStates(_statesBuffer, indices.Length, _indicesBuffer, _statesCopyBuffer);
        }

        public void Update()
        {
            _functions.UpdateBullets(_statesBuffer, _optionsBuffer);
            _emptyIndexQueue.Update();
        }

        public void Render()
        {
            _functions.RenderBullets(_statesBuffer, _optionsBuffer);
        }

        public void Release()
        {
            _statesBuffer.Release();
            _optionsBuffer.Release();
            _emptyIndexQueue.Release();
            _statesCopyBuffer.Release();
            _optionsCopyBuffer.Release();
            _indicesBuffer.Release();
        }

        public int GetRestAddlessSize()
        {
            return _emptyIndexQueue.RestQueue;
        }

        private void AddStates(ComputeBuffer buffer,int n, ComputeBuffer indicesBuffer, ComputeBuffer copyBuffer)
        {
            int kernel = _emptyComputeShader.FindKernel("AddStates");
            _emptyComputeShader.SetInt("N", n);
            _emptyComputeShader.SetBuffer(kernel, "States",buffer);
            _emptyComputeShader.SetBuffer(kernel, "InoutBuffer", indicesBuffer);
            _emptyComputeShader.SetBuffer(kernel, "StateInputs", copyBuffer);
            _emptyComputeShader.Dispatch(kernel, (n - 1) / 8 + 1, 1, 1);
        }
    }
}
