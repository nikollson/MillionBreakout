
using System.Runtime.InteropServices;
using Stool.MilllionBullets.Implements;
using UnityEngine;

namespace Stool.MilllionBullets
{
    struct State
    {
        public Vector3 Position;
        public int Enable;

        public State(Vector3 position, int enable)
        {
            Position = position;
            Enable = enable;
        }
    }

    class BulletsBuffer<TOption> where TOption:struct 
    {
        private BufferFunctionsBase<TOption> _functions;

        private EmptyIndexQueue _emptyIndexQueue;
        private ComputeBuffer _optionsBuffer;
        private ComputeBuffer _statesBuffer;

        public BulletsBuffer(BufferFunctionsBase<TOption> functions, ComputeShader emptyComputeShader)
        {
            var length = functions.GetLength();
            _functions = functions;

            _emptyIndexQueue = new EmptyIndexQueue(emptyComputeShader, length);
            _statesBuffer = new ComputeBuffer(length, Marshal.SizeOf(typeof(State)));
            _optionsBuffer = new ComputeBuffer(length, Marshal.SizeOf(typeof(TOption)));
        }

        public void AddBullets(State[] states, TOption[] options)
        {
            var indices = _emptyIndexQueue.GetEmptyIndices(states.Length);

            _functions.AddOptions(_optionsBuffer, indices, options);
            AddStates(_statesBuffer, indices, states);
        }

        public void Update()
        {
            _functions.UpdateBullets(_statesBuffer, _optionsBuffer);
        }

        public void Render()
        {
            _functions.RenderBullets(_statesBuffer, _optionsBuffer);
        }

        public void Release()
        {
            _statesBuffer.Release();
            _optionsBuffer.Release();
        }

        private void AddStates(ComputeBuffer buffer, int[] indices, State[] states)
        {
            var array = new State[buffer.count];
            buffer.GetData(array);
            for (int i = 0; i < indices.Length; i++)
            {
                array[indices[i]] = states[indices[i]];
            }
            buffer.SetData(array);
        }
    }
}
