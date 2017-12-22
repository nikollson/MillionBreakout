
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
            OnDestroy(this);
        }

        public abstract CircleCollisionSearcher.CheckState AreaCheck(Rectangle area, float currentWidth);

        public abstract DistanceInfo2D CircleCheck(ICircleCollider collider);
    }
}
