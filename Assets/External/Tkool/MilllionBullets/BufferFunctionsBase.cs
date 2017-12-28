
using System;
using UnityEngine;

namespace Tkool.MilllionBullets
{
    public abstract class BufferFunctionsBase<TOption> : MonoBehaviour where TOption:struct 
    {
        public virtual void AddOptions(ComputeBuffer optionsBuffer, int n, ComputeBuffer indicesBuffer, ComputeBuffer inputBuffer) 
        {
            Debug.LogError("Not override AddData");
        }

        public virtual void RenderBullets(ComputeBuffer bulletsBuffer, ComputeBuffer optionsBuffer)
        {
            Debug.LogError("Not override Render");
        }

        public virtual void UpdateBullets(ComputeBuffer bulletsBuffer, ComputeBuffer optionsBuffer)
        {
            Debug.LogError("Not override OnUpdateBullet");
        }

        public virtual int GetLength()
        {
            Debug.LogError("Not override GetLength");
            return 0;
        }

        public virtual int GetCopyBufferLength()
        {
            return Math.Max(100, GetLength() / 20);
        }
    }
}
