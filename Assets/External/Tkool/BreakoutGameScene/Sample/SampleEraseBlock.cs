
using Stool.Algorithm.Geometry;
using UnityEngine;

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
