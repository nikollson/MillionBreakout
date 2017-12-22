
using Stool.Algorithm.Geometry;

namespace Tkool.BreakoutGameScene.Sample
{
    class SampleEraseBlock : BreakoutBoxBlockBehaviour
    {
        public override void OnBallCollide(BreakoutBallBehaviour ball, DistanceInfo2D distanceInfo)
        {
            ball.Destroy();
        }
    }
}
