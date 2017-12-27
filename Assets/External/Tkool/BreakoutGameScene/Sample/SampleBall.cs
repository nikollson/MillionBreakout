
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene.Sample
{
    class SampleBall : BreakoutBallBehaviour
    {
        public Texture2D Texture;

        public SampleBall(float radius, Vector2 velocity, Texture2D texture) : base(radius, velocity)
        {
            Velocity = velocity;
            Texture = texture;
        }

        public override IBallCollisionEffect GetCollisionEffect()
        {
            return new SampleBallCollisionEffect();
        }

        public override void OnCollision(CircleCollisionInfo collision, IBlockCollisionEffect blockHitEffect)
        {
            var effect = (SampleBlockCollisionEffect) blockHitEffect;
        }
    }
}
