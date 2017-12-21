
namespace Stool.Algorithm.Geometry
{
    class CircleCollisionInfo
    {
        public ICircleCollider Collider { get; private set; }
        public DistanceInfo2D DistanceInfo { get; private set; }

        public CircleCollisionInfo(ICircleCollider collider, DistanceInfo2D distanceInfo)
        {
            Collider = collider;
            DistanceInfo = distanceInfo;
        }
    }
}
