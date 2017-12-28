
using System.Collections.Generic;
using Stool.Algorithm.Geometry;

namespace Tkool.BreakoutGameScene
{
    public class BreakoutBlockCollisionInfo : CircleCollisionInfo
    {
        public List<GridInfo> GridData { get; private set; }

        public BreakoutBlockCollisionInfo(ICircleCollider collider)
            : base(collider, new DistanceInfo2D(float.PositiveInfinity, 0))
        {
            GridData = new List<GridInfo>();
        }

        public void MergeInfo(DistanceInfo2D distanceInfo, int arrayX, int arrayY)
        {
            if (Distance > distanceInfo.Distance)
            {
                UpdateDistance(distanceInfo.Distance);
                UpdateAngle(distanceInfo.Angle);
            }

            if (distanceInfo.Distance <= 0)
            {
                GridData.Add(new GridInfo(Collider, distanceInfo, arrayX, arrayY));
            }
        }

        public class GridInfo
        {
            public int ArrayX { get; private set; }
            public int ArrayY { get; private set; }
            public CircleCollisionInfo Collision { get; private set; }

            public GridInfo(ICircleCollider collider, DistanceInfo2D distanceInfo, int arrayX, int arrayY)
            {
                Collision = new CircleCollisionInfo(collider,distanceInfo);
                ArrayX = arrayX;
                ArrayY = arrayY;
            }
        }

    }
}
