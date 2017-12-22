
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class BreakoutGridBlockBehaviour : BreakoutBoxBlockBehaviour
    {
        public MeshRenderer MeshRenderer;
        public MeshFilter MeshFilter;
        public int ArrayWidth = 5;
        public int ArrayHeight = 5;

        public bool[,] EnableArray
        {
            get
            {
                if (_enableArray == null)
                {
                    _enableArray = new bool[ArrayHeight, ArrayWidth];
                    for (int i = 0; i < ArrayHeight; i++)
                    {
                        for (int j = 0; j < ArrayWidth; j++)
                        {
                            _enableArray[i, j] = true;
                        }
                    }
                }

                return _enableArray;
            }
        }

        bool[,] _enableArray;

        public virtual void OnWillRenderObject()
        {
            MeshRenderer.material.SetInt("_ArrayWidth", ArrayWidth);
            MeshRenderer.material.SetInt("_ArrayHeight", ArrayHeight);

            var eraseArray = new float[EnableArray.Length];
            for (int i = 0; i < ArrayHeight; i++)
            {
                for (int j = 0; j < ArrayWidth; j++)
                {
                    int index = i * ArrayWidth + j;
                    eraseArray[index] = EnableArray[i, j] ? 0 : 1;
                }
            }
            MeshRenderer.material.SetFloatArray("_EraseArray", eraseArray);
        }
        
        public override void OnBallCollide(BreakoutBallBehaviour ball, DistanceInfo2D distanceInfo)
        {
            var info = (BreakoutGridBlockCollision.GridBlockDistanceInfo)distanceInfo;

            foreach(var a in info.InfoList)
            {
                EnableArray[a.ArrayY, a.ArrayX] = false;
            }
        }

        public override DistanceInfo2D CircleCollision_ColliderCheck(ICircleCollider circleCollider)
        {
            return BreakoutGridBlockCollision.CheckHitCircle(Rectangle, EnableArray, circleCollider);
        }
    }
}