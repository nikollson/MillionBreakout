using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{

    public class TestBall : BallBehaviour
    {
        public TestBall(BallCollisionEffect effect, int hp, float radius, Vector2 velocity) 
            : base(effect, hp, radius, velocity)
        {

        }
    }
}