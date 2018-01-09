
using Stool.Algorithm.Geometry;
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    public abstract class BallBehaviour : BreakoutBallBehaviour
    {
        public BallCollisionEffect CollisionEffect;

        public int HP;

        protected BallBehaviour(BallCollisionEffect effect, int hp, float radius, Vector2 velocity) : base(radius, velocity)
        {
            HP = hp;
            CollisionEffect = effect;
        }

        public override void OnCollision(CircleCollisionInfo collision, IBlockCollisionEffect blockHitEffect)
        {
            var effect = blockHitEffect as BlockCollisionEffect;

            if (effect.DoErase)
            {
                Destroy();
            }

            if (effect.RecieveDamage)
            {
                HP -= Mathf.Min(HP, Mathf.Min(effect.RecieveDamageMax, CollisionEffect.Attack));
            }

            if (effect.DoSpoit)
            {
                ButtleSystem.WaterSystem.AddSnow(Transform.position, HP);
                Destroy();
            }

            if (HP == 0)
            {
                Destroy();
            }
        }

        public override IBallCollisionEffect GetCollisionEffect()
        {
            return CollisionEffect;
        }
    }
}
