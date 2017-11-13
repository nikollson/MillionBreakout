
using UnityEngine;

namespace Stool.MilllionBullets
{
    abstract class BufferFunctionsBase<TOption> : MonoBehaviour where TOption:struct 
    {
        public virtual void AddOptions(ComputeBuffer optionsBuffer, int[] indices, TOption[] options) 
        {
            Debug.LogError("Not override AddData");
        }

        public virtual void RenderBullets(ComputeBuffer bulletsBuffer, ComputeBuffer optionsBuffer)
        {
            Debug.LogError("Not override Render");
        }

        public virtual void UpdateBullets(ComputeBuffer bulletsBuffer, ComputeBuffer optionsBuffer)
        {
            Debug.LogError("Not override Update");
        }

        public virtual int GetLength()
        {
            Debug.LogError("Not override GetLength");
            return 0;
        }
    }
}
