
using System.Runtime.InteropServices;
using UnityEngine;
using Stool.CSharp;

namespace StoolKit.MilllionBullets.Buffer
{
    class BulletsBufferAdder<TOption> where TOption:struct
    {
        public int CopyBufferLength { get; private set; }
        public EmptyIndexQueue EmptyIndexQueue { get; private set; }

        private CommonData<TOption> _common;
        private ComputeShader _basicComputeShader;

        private ComputeBuffer _indicesBuffer;
        private ComputeBuffer _optionsCopyBuffer;
        private ComputeBuffer _statesCopyBuffer;

        private readonly int ThreadNum = 32;

        public BulletsBufferAdder(CommonData<TOption> common, ComputeShader basicComputeShader)
        {
            CopyBufferLength = common.ControlFunctions.GetCopyBufferLength();
            _common = common;
            _basicComputeShader = basicComputeShader;

            _indicesBuffer = new ComputeBuffer(common.Length, sizeof(int));
            _statesCopyBuffer = new ComputeBuffer(CopyBufferLength, Marshal.SizeOf(typeof(BulletState)));
            _optionsCopyBuffer = new ComputeBuffer(CopyBufferLength, Marshal.SizeOf(typeof(TOption)));
            EmptyIndexQueue = new EmptyIndexQueue(basicComputeShader, _common.StatesBuffer, _common.Length);
        }

        public void AddBullets(BulletState[] states, TOption[] options)
        {
            var indices = EmptyIndexQueue.PopEmptyIndices(states.Length);
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

            _common.ControlFunctions.AddOptions(_common.OptionsBuffer, indices.Length, _indicesBuffer, _optionsCopyBuffer);
            AddStates(_common.StatesBuffer, indices.Length, _indicesBuffer, _statesCopyBuffer);
        }

        private void AddStates(ComputeBuffer buffer, int n, ComputeBuffer indicesBuffer, ComputeBuffer copyBuffer)
        {
            int kernel = _basicComputeShader.FindKernel("AddStates");
            _basicComputeShader.SetInt("N", n);
            _basicComputeShader.SetBuffer(kernel, "States", buffer);
            _basicComputeShader.SetBuffer(kernel, "InoutBuffer", indicesBuffer);
            _basicComputeShader.SetBuffer(kernel, "StateInputs", copyBuffer);
            _basicComputeShader.Dispatch(kernel, (n - 1) / ThreadNum + 1, 1, 1);
        }

        public void Release()
        {
            EmptyIndexQueue.Release();
            _statesCopyBuffer.Release();
            _optionsCopyBuffer.Release();
            _indicesBuffer.Release();
        }
    }
}
