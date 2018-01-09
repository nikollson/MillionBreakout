
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public abstract class BreakoutBlockBehaviour : MonoBehaviour
    {

        public BreakoutBlockCollider BlockCollider
        {
            get
            {
                if (_blockCollider == null)
                {
                    Debug.LogError(gameObject.name + " のBlockColliderがNullです");
                }
                return _blockCollider;
            }
        }

        [SerializeField]
        private BreakoutBlockCollider _blockCollider;


        public bool IsDestroyed { get; private set; }
       
        public void Destroy()
        {
            IsDestroyed = true;
        }

        public void OnRenderBase()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null || meshRenderer.enabled == false) return;

            var material = meshRenderer.material;

            if (BlockCollider != null)
            {
                var blockArray = BlockCollider.EnableArray;

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
            }

            OnRender(material);
        }

        public virtual void OnRender(Material material)
        {
            
        }

        public virtual bool CanCollision(BreakoutBallBehaviour ball)
        {
            return true;
        }

        public abstract IBlockCollisionEffect GetCollisionEffect(int arrayX, int arrayY);

        public abstract void OnCollision(
            int arrayX, int arrayY,
            CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect);
    }
}
