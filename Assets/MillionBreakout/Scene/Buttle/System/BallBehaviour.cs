
using Stool.Algorithm.Geometry;
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    public class BallBehaviour : BreakoutBallBehaviour
    {
        public BallBehaviour(float radius, Vector2 velocity, Texture2D texture) : base(radius, velocity)
        {
            
        }

        public override IBallCollisionEffect GetCollisionEffect()
        {
            throw new System.NotImplementedException();
        }

        public override void OnCollision(CircleCollisionInfo collision, IBlockCollisionEffect blockHitEffect)
        {
            throw new System.NotImplementedException();
        }
    }
}
