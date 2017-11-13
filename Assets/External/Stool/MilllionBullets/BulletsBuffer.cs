
using System.Runtime.InteropServices;
using Stool.MilllionBullets.Buffer;
using UnityEngine;

namespace Stool.MilllionBullets
{
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

            _statesBuffer = new ComputeBuffer(length, Marshal.SizeOf(typeof(BulletState)));
            _optionsBuffer = new ComputeBuffer(length, Marshal.SizeOf(typeof(TOption)));
            _emptyIndexQueue = new EmptyIndexQueue(emptyComputeShader, _statesBuffer, length);
        }

        public void AddBullets(BulletState[] states, TOption[] options)
        {
            var indices = _emptyIndexQueue.PopEmptyIndices(states.Length);
            _functions.AddOptions(_optionsBuffer, indices, options);
            AddStates(_statesBuffer, indices, states);
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
        }

        public int GetRestAddlessSize()
        {
            return _emptyIndexQueue.RestQueue;
        }

        private void AddStates(ComputeBuffer buffer, int[] indices, BulletState[] states)
        {
            var array = new BulletState[buffer.count];
            buffer.GetData(array);
            for (int i = 0; i < indices.Length; i++)
            {
                array[indices[i]] = states[i];
            }
            buffer.SetData(array);
        }
    }
}
