
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    public abstract class BreakoutBlockBehaviour : MonoBehaviour
    {
        public MeshRenderer MeshRenderer;

        public bool IsDestroyed { get; private set; }
       
        public void Destroy()
        {
            IsDestroyed = true;
        }

        public void OnRenderBase()
        {
            var material = MeshRenderer.material;

            var blockArray = GetBreakoutBlockCollider().EnableArray;

            int width = blockArray.GetLength(1);
            int height = blockArray.GetLength(0);

            var eraseArray = new float[blockArray.Length];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = i * width + j;
                    eraseArray[index] = blockArray[i, j] ? 0 : 1;
                }
            }

            material.SetInt("ArrayWidth", width);
            material.SetInt("ArrayHeight", height);
            material.SetFloatArray("EraseArray", eraseArray);

            OnRender(material);
        }

        public virtual void OnRender(Material material)
        {
            
        }

        public abstract IBlockCollisionEffect GetCollisionEffect();

        public abstract void OnCollision(
            int arrayX, int arrayY,
            CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect);

        public abstract BreakoutBlockCollider GetBreakoutBlockCollider();
    }
}
