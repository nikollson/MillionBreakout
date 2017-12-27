
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    abstract class BreakoutBlockCollider : ICircleCollisionChecker
    {
        public bool DoCheckCollision { get; private set; }
        public bool[,] EnableArray { get; set; }

        public BreakoutBlockCollider(bool[,] enableArray)
        {
            DoCheckCollision = true;
            EnableArray = enableArray;
        }

        public abstract CircleCollisionSearcher.CheckState
            CircleCollision_AreaCheck(Rectangle area, float currentWidth);

        public abstract CircleCollisionInfo CircleCollision_ColliderCheck(ICircleCollider circleCollider);
    }
}
