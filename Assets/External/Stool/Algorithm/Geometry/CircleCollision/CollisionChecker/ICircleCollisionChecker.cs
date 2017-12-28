
namespace Stool.Algorithm.Geometry
{
    public interface ICircleCollisionChecker
    {
        CircleCollisionSearcher.CheckState CircleCollision_AreaCheck(Rectangle area, float currentWidth);
        CircleCollisionInfo CircleCollision_ColliderCheck(ICircleCollider circleCollider);
    }
}
