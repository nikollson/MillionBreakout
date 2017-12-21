
using UnityEngine;

namespace Tkool.MilllionBullets.Buffer
{
    class Updater<TOption> where TOption : struct
    {
        private CommonData<TOption> _common;
        private ComputeShader _computeShader;

        private readonly int ThreadNum = 32;

        public Updater(CommonData<TOption> common, ComputeShader computeShader)
        {
            _common = common;
            _computeShader = computeShader;
        }

        public void Update(float deltaTime)
        {
            int kernel = _computeShader.FindKernel("UpdateState");
            _computeShader.SetFloat("DeltaTime", deltaTime);
            _computeShader.SetBuffer(kernel, "States", _common.StatesBuffer);
            _computeShader.Dispatch(kernel, (_common.StatesBuffer.count - 1) / ThreadNum + 1, 1, 1);
        }
    }
}
