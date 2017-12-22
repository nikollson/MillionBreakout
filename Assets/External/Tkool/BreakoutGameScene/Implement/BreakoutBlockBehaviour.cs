
using System;
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    abstract class BreakoutBlockBehaviour : MonoBehaviour , ICircleCollisionChecker
    {
        public Action<BreakoutBlockBehaviour> OnDestroy;

        public virtual void OnBallCollide(BreakoutBallBehaviour ball, DistanceInfo2D distanceInfo)
        {

        }

        public virtual bool CanCollision(BreakoutBallBehaviour ball)
        {
            return true;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            OnDestroy(this);
        }

        public abstract CircleCollisionSearcher.CheckState CircleCollision_AreaCheck(Rectangle area, float currentWidth);

        public abstract DistanceInfo2D CircleCollision_ColliderCheck(ICircleCollider collider);
    }
}
