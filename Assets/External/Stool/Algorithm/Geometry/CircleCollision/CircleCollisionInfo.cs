
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    public class CircleCollisionInfo
    {
        public ICircleCollider Collider { get; private set; }
        public float Distance { get; private set; }
        public Vector2 HitPosition { get; private set; }
        public float AngleToCircle { get; private set; }
        public float AngleToOpponent { get; private set; }

        public bool IsHit { get { return Distance <= 0; } }

        public CircleCollisionInfo(ICircleCollider collider, DistanceInfo2D distanceInfo)
        {
            Collider = collider;
            HitPosition = CalcHitPosition(distanceInfo);

            UpdateDistance(distanceInfo.Distance);
            UpdateAngle(distanceInfo.Angle);
        }

        public void UpdateDistance(float distance)
        {
            Distance = distance;
        }

        public void UpdateAngle(float angleToOpponent)
        {
            AngleToCircle = angleToOpponent - (angleToOpponent > 0 ? 1 : -1) * Mathf.PI;
            AngleToOpponent = angleToOpponent;
        }

        private Vector2 CalcHitPosition(DistanceInfo2D distanceInfo)
        {
            float dx = Mathf.Cos(distanceInfo.Angle);
            float dy = Mathf.Sin(distanceInfo.Angle);

            return new Vector2(dx, dy) * (Collider.GetColliderRadius() + distanceInfo.Distance * 0.5f);
        }
    }
}
