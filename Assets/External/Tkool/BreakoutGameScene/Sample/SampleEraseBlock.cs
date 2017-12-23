
using System;
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene.Sample
{
    class SampleEraseBlock : BreakoutBoxBlockBehaviour
    {
        public override BreakoutBallCollisionEffect MakeBallCollisionEffect(DistanceInfo2D distanceInfo2D)
        {
            var ret = new BreakoutBallCollisionEffect(distanceInfo2D);
            ret.DoDestroy = true;
            return ret;
        }

        public override void RecieveCollisionEffect(BreakoutBlockCollisionEffect effect)
        {
            effect.DoDestroy = false;
            base.RecieveCollisionEffect(effect);
        }
    }
}
