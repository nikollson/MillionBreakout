using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{

    public class TestBall : BallBehaviour
    {
        public TestBall(float radius, Vector2 velocity) : base(radius, velocity)
        {

        }

        public override IBallCollisionEffect GetCollisionEffect()
        {
            return new BallCollisionEffect();
        }
    }
}