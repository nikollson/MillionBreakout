
using System;
using System.Runtime.InteropServices;
using Stool.CodingSupport;
using UnityEngine;

namespace Stool.MilllionBullets.Collision2D
{
    class Searcher
    {
        public enum BoxColliderMode { Normal, PlayerBar , EraseBall};

        private CommonData _common;
        private ComputeShader _computeShader;
        private ComputeBuffer _inoutIntBuffer;
        private int[] _inoutIntArray;

        private readonly int ThreadNum = 32;

        public Searcher(CommonData common, ComputeShader computeShader)
        {
            _common = common;
            _computeShader = computeShader;
            _inoutIntBuffer = new ComputeBuffer(50, sizeof(int));
            _inoutIntArray = new int[_inoutIntBuffer.count];
        }

        private int _boxKernelId = 0;
        public void CheckBoxCollision(MillionBulletsBoxCollider boxCollider, BoxColliderMode mode = BoxColliderMode.Normal)
        {
            var box = boxCollider.GetBox();

            int kernel = _computeShader.FindKernel("CheckBoxCollision" + _boxKernelId);
            _boxKernelId = (_boxKernelId + 1) % 2;

            _computeShader.SetVector("BoxCenter", box.Center);
            _computeShader.SetFloat("BoxAngle", box.Angle);
            _computeShader.SetFloat("BoxWidth", box.Width);
            _computeShader.SetFloat("BoxHeight", box.Height);
            _computeShader.SetInt("NotBackReflection", mode == BoxColliderMode.PlayerBar ? 1 : 0);
            _computeShader.SetInt("EraseBall", mode == BoxColliderMode.EraseBall ? 1 : 0);
            _computeShader.SetBuffer(kernel, "InoutInt", _inoutIntBuffer);
            _computeShader.SetFloat("DeadLineY", ButtleSystem.Instance.Stage.EnemyDeadLineY);

            _inoutIntArray.Fill(0);
            _inoutIntBuffer.SetData(_inoutIntArray);

            foreach (var buffer in _common.BulletsBuffers)
            {
                _computeShader.SetBuffer(kernel, "States", buffer);
                _computeShader.Dispatch(kernel, (buffer.count - 1) / ThreadNum + 1, 1, 1);
            }

            _inoutIntBuffer.GetData(_inoutIntArray);
            boxCollider.AddHitCount(_inoutIntArray[0]);
            if (_inoutIntArray[1] != 0)
            {
                boxCollider.OverDeadLine(_inoutIntArray[1]);
            }
        }

        private int _blocksKernelId = 0;
        public void CheckBlocksCollision(MillionBulletsBlocksCollider blocksCollider, BoxColliderMode mode = BoxColliderMode.Normal)
        {
            var info = blocksCollider.GetBlocksInfo();
            var box = blocksCollider.GetBox();
            int kernel = _computeShader.FindKernel("CheckBlocksCollision"+_blocksKernelId);
            _blocksKernelId = (_blocksKernelId + 1) % 2;

            _computeShader.SetVector("BoxCenter", box.Center);
            _computeShader.SetFloat("BoxAngle", box.Angle);
            _computeShader.SetFloat("BoxWidth", box.Width);
            _computeShader.SetFloat("BoxHeight", box.Height);
            _computeShader.SetFloat("DivideX", 1.0f / info.ArrayWidth);
            _computeShader.SetFloat("DivideY", 1.0f / info.ArrayHeight);
            _computeShader.SetInt("ArrayWidth", info.ArrayWidth);
            _computeShader.SetInt("ArrayHeight", info.ArrayHeight);
            _computeShader.SetInt("NotBackReflection", mode == BoxColliderMode.PlayerBar ? 1 : 0);
            _computeShader.SetInt("EraseBall", mode == BoxColliderMode.EraseBall ? 1 : 0);
            _computeShader.SetFloat("DeadLineY", ButtleSystem.Instance.Stage.EnemyDeadLineY);

            _computeShader.SetBuffer(kernel, "InoutInt", _inoutIntBuffer);
            _computeShader.SetBuffer(kernel,"BlockElements", blocksCollider.BlockElementsBuffer);

            _inoutIntArray.Fill(0);
            _inoutIntBuffer.SetData(_inoutIntArray);

            foreach (var buffer in _common.BulletsBuffers)
            {
                _computeShader.SetBuffer(kernel, "States", buffer);
                _computeShader.Dispatch(kernel, (buffer.count - 1) / ThreadNum + 1, 1, 1);
            }

            _inoutIntBuffer.GetData(_inoutIntArray);
            blocksCollider.AddHitCount(_inoutIntArray[0]);
            if (_inoutIntArray[1] != 0)
            {
                blocksCollider.OverDeadLine(_inoutIntArray[1]);
            }
        }

        public void Release()
        {
            _inoutIntBuffer.Release();
        }
    }
}
