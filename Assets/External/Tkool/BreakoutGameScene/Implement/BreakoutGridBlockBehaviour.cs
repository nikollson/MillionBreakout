
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    abstract class BreakoutGridBlockBehaviour : BreakoutBoxBlockBehaviour
    {
        public MeshRenderer MeshRenderer;

        public abstract IBreakoutGridBlockData[,] GetBlockArray();

        public void OnWillRenderObject()
        {
            var blockData = GetBlockArray();
            OnPrepareMaterial(MeshRenderer.material, blockData);

            bool hasEnable = false;
            foreach(var a in blockData)
            {
                hasEnable |= a.IsEnable();
            }

            if(hasEnable==false)
            {
                Destroy();
            }
        }

        public virtual void OnPrepareMaterial(Material material, IBreakoutGridBlockData[,] blockArray)
        {
            int width = blockArray.GetLength(1);
            int height = blockArray.GetLength(0);

            var eraseArray = new float[blockArray.Length];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = i * width + j;
                    eraseArray[index] = blockArray[i, j].IsEnable() ? 0 : 1;
                }
            }

            material.SetInt("ArrayWidth", width);
            material.SetInt("ArrayHeight", height);
            material.SetFloatArray("EraseArray", eraseArray);
        }

        public override DistanceInfo2D CircleCollision_ColliderCheck(ICircleCollider circleCollider)
        {
            return BreakoutGridBlockCollision.CheckHitCircle(Rectangle, GetBlockArray(), circleCollider);
        }

        public override void RecieveCollisionEffect(BreakoutBlockCollisionEffect effect)
        {
            var info = (GridBlockDistanceInfo)effect.DistanceInfo;
            RecieveCollisionEffectGrid(effect, info);
        }

        public abstract void RecieveCollisionEffectGrid(BreakoutBlockCollisionEffect effect, GridBlockDistanceInfo distanceInfo);
    }
}