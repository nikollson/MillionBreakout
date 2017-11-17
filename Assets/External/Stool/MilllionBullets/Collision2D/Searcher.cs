
using System.Runtime.InteropServices;
using UnityEngine;

namespace Stool.MilllionBullets.Collision2D
{
    class Searcher
    {
        private CommonData _common;
        private ComputeShader _computeShader;

        private readonly int ThreadNum = 32;

        public Searcher(CommonData common, ComputeShader computeShader)
        {
            _common = common;
            _computeShader = computeShader;
        }

        public void CheckBoxCollision(MillionBulletsBoxCollider boxCollider)
        {
            var box = boxCollider.GetBox();
            int kernel = _computeShader.FindKernel("CheckBoxCollision");

            foreach (var buffer in _common.BulletsBuffers)
            {
                _computeShader.SetVector("BoxCenter", box.Center);
                _computeShader.SetFloat("BoxAngle", box.Angle);
                _computeShader.SetFloat("BoxWidth", box.Width);
                _computeShader.SetFloat("BoxHeight", box.Height);
                _computeShader.SetBuffer(kernel, "States", buffer);
                _computeShader.Dispatch(kernel, (buffer.count - 1) / ThreadNum + 1, 1, 1);
            }
        }

        public void CheckBlocksCollision(MillionBulletsBlocksCollider blocksCollider)
        {
            var info = blocksCollider.GetBlocksInfo();
            var box = blocksCollider.GetBox();
            int kernel = _computeShader.FindKernel("CheckBlocksCollision");

            _computeShader.SetVector("BoxCenter", box.Center);
            _computeShader.SetFloat("BoxAngle", box.Angle);
            _computeShader.SetFloat("BoxWidth", box.Width);
            _computeShader.SetFloat("BoxHeight", box.Height);
            _computeShader.SetFloat("DivideX", 1.0f / info.ArrayWidth);
            _computeShader.SetFloat("DivideY", 1.0f / info.ArrayHeight);
            _computeShader.SetInt("ArrayWidth", info.ArrayWidth);
            _computeShader.SetInt("ArrayHeight", info.ArrayHeight);
            _computeShader.SetBuffer(kernel,"BlockElements", blocksCollider.BlockElementsBuffer);

            foreach (var buffer in _common.BulletsBuffers)
            {
                _computeShader.SetBuffer(kernel, "States", buffer);
                _computeShader.Dispatch(kernel, (buffer.count - 1) / ThreadNum + 1, 1, 1);
            }
        }

        public void Release()
        {
        }
    }
}
