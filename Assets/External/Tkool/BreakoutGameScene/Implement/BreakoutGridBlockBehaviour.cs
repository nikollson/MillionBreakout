
using Stool.Algorithm.Geometry;

namespace Tkool.BreakoutGameScene
{
    class BreakoutGridBlockBehaviour : BreakoutBoxBlockBehaviour
    {
        public override DistanceInfo2D CircleCheck(ICircleCollider collider)
        {
            float distance = 0;
            float angle = 0;




            return new DistanceInfo2D(distance, angle);
        }

        public virtual bool GetLivingState()
        {
            return false;
        }
    }
}