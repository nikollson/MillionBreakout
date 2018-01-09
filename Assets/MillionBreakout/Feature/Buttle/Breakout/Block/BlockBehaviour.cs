
using System.Diagnostics;
using Stool.Algorithm.Geometry;
using Tkool.BreakoutGameScene;

namespace MillionBreakout
{
    public abstract class BlockBehaviour : BreakoutBlockBehaviour
    {
        public BlockCollisionEffect CollisionEffect;

        public void Awake()
        {
            
        }

        public void Start()
        {
            ButtleSystem.Breakout.AddBlock(this);
        }

        public override void OnCollision(int arrayX, int arrayY, CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect)
        {
            var effect = ballHitEffect as BallCollisionEffect;

            if (effect.DoErase)
            {
                this.Destroy();
            }

            if (BlockCollider is TextureBlockCollider)
            {
                var collider = BlockCollider as TextureBlockCollider;

                if (effect.Attack != 0)
                {
                    collider.AddDamage(arrayX, arrayY, effect.Attack);

                    ButtleSystem.WaterSystem.MakeWater(transform.position, effect.Attack);
                }
            }
        }

        public override IBlockCollisionEffect GetCollisionEffect(int arrayX, int arrayY)
        {
            int maxDamage = 10000;

            if (BlockCollider is TextureBlockCollider)
            {
                maxDamage = (BlockCollider as TextureBlockCollider).HP[arrayY, arrayX];
            }

            CollisionEffect.RecieveDamageMax = maxDamage;

            return CollisionEffect;
        }
    }
}
