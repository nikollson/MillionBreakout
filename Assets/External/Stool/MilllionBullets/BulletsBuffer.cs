
using Stool.MilllionBullets.Buffer;
using UnityEngine;

namespace Stool.MilllionBullets
{
    class BulletsBuffer<TOption> where TOption:struct
    {
        public CommonData<TOption> Data { get; private set; }
        public BulletsBufferAdder<TOption> Adder { get; private set; }
        private Updater<TOption> _updater;
        
        public BulletsBuffer(BufferFunctionsBase<TOption> functions, ComputeShader basicComputeShader)
        {
            Data = new CommonData<TOption>(functions, basicComputeShader);
            Adder = new BulletsBufferAdder<TOption>(Data, basicComputeShader);
            _updater = new Updater<TOption>(Data,basicComputeShader);
        }

        public void Update(float deltaTime)
        {
            Data.ControlFunctions.UpdateBullets(Data.StatesBuffer, Data.OptionsBuffer);
            Adder.EmptyIndexQueue.Update();
            _updater.Update(deltaTime);
        }

        public void Render()
        {
            Data.ControlFunctions.RenderBullets(Data.StatesBuffer, Data.OptionsBuffer);
        }

        public void Release()
        {
            Adder.Release();
            Data.Release();
        }

        public int GetRestAddlessSize()
        {
            return Adder.EmptyIndexQueue.RestQueue;
        }
    }
}
