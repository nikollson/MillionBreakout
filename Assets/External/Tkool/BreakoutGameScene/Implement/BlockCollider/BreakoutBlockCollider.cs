
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    public abstract class BreakoutBlockCollider : MonoBehaviour, ICircleCollisionChecker
    {
        public bool DontCheckCollision { get; set; }
        public bool[,] EnableArray { get; set; }

        public abstract CircleCollisionSearcher.CheckState
            CircleCollision_AreaCheck(Rectangle area, float currentWidth);

        public abstract CircleCollisionInfo CircleCollision_ColliderCheck(ICircleCollider circleCollider);

        public bool IsEnable()
        {
            bool ret = false;

            foreach (var b in EnableArray)
            {
                if (b == true)
                    ret = true;
            }

            return ret;
        }
    }
}
