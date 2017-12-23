
using Stool.Algorithm.Geometry;

namespace Tkool.BreakoutGameScene
{
    class BreakoutBlockCollisionEffect
    {
        public DistanceInfo2D DistanceInfo;
        public bool DoDestroy;

        public BreakoutBlockCollisionEffect(DistanceInfo2D distanceInfo)
        {
            DistanceInfo = distanceInfo;
        }
    }
}
