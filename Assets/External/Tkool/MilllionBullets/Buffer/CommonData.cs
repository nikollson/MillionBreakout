
using System.Runtime.InteropServices;
using UnityEngine;

namespace Tkool.MilllionBullets.Buffer
{
    public class CommonData<TOption> where TOption:struct
    {
        public int Length { get; private set; }
        public BufferFunctionsBase<TOption> ControlFunctions { get; private set; }
        public ComputeBuffer OptionsBuffer { get; private set; }
        public ComputeBuffer StatesBuffer { get; private set; }

        public CommonData(BufferFunctionsBase<TOption> functions, ComputeShader basicComputeShader)
        {
            Length = functions.GetLength();
            ControlFunctions = functions;

            StatesBuffer = new ComputeBuffer(Length, Marshal.SizeOf(typeof(BulletState)));
            OptionsBuffer = new ComputeBuffer(Length, Marshal.SizeOf(typeof(TOption)));
        }

        public void Release()
        {
            StatesBuffer.Release();
            OptionsBuffer.Release();
        }
    }
}
