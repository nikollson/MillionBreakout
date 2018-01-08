
using Stool.Algorithm.Geometry;
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    public abstract class BallBehaviour : BreakoutBallBehaviour
    {
        public BallCollisionEffect CollisionEffect;

        public int HP = 1;

        protected BallBehaviour(float radius, Vector2 velocity) : base(radius, velocity)
        {

        }

        public override void OnCollision(CircleCollisionInfo collision, IBlockCollisionEffect blockHitEffect)
        {
            var effect = blockHitEffect as BlockCollisionEffect;

            if (effect.DoErase)
            {
                Destroy();
            }

            if (effect.Attack != 0)
            {
                HP -= Mathf.Min(HP, effect.Attack);
            }
        }

        public override IBallCollisionEffect GetCollisionEffect()
        {
            return CollisionEffect;
        }
    }
}
