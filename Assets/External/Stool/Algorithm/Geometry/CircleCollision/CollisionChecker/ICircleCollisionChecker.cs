
namespace Stool.Algorithm.Geometry
{
    interface ICircleCollisionChecker
    {
        CircleCollisionSearcher.CheckState CircleCollision_AreaCheck(Rectangle area, float currentWidth);
        DistanceInfo2D CircleCollision_ColliderCheck(ICircleCollider circleCollider);
    }
}
