
namespace Stool.Algorithm.Geometry
{
    interface ICircleCollisionChecker
    {
        CircleCollisionSearcher.CheckState AreaCheck(Rectangle area, float currentWidth);
        DistanceInfo2D CircleCheck(ICircleCollider circleCollider);
    }
}
