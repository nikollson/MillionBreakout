
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class BreakoutBallCollisionEffect
    {
        public DistanceInfo2D DistanceInfo;
        public float PositionCorrectRate = 1;
        public bool DoDestroy;

        public BreakoutBallCollisionEffect(DistanceInfo2D distanceInfo)
        {
            DistanceInfo = distanceInfo;
        }

        public Vector2 GetPositionCorrectDistance()
        {
            Vector2 reflectDir = new Vector2(Mathf.Cos(DistanceInfo.Angle), Mathf.Sin(DistanceInfo.Angle));
            return PositionCorrectRate * DistanceInfo.Distance * reflectDir * -1;
        }

        public Vector2 GetReflectedVelocity(Vector2 currentVelocity)
        {
            Vector2 reflectDir = new Vector2(Mathf.Cos(DistanceInfo.Angle), Mathf.Sin(DistanceInfo.Angle));
            float velocityDot = Mathf.Min(0, Vector3.Dot(reflectDir, currentVelocity));
            return currentVelocity + velocityDot * reflectDir * -2;
        }
    }
}
