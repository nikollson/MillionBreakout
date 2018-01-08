
using Stool.Algorithm.Geometry;
using Tkool.BreakoutGameScene;

namespace MillionBreakout
{
    public class ReflectBlock : BlockBehaviour
    {
        public override void OnCollision(int arrayX, int arrayY, CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect)
        {
            base.OnCollision(arrayX, arrayY, collision, ballHitEffect);
        }

        public override IBlockCollisionEffect GetCollisionEffect()
        {
            return new BlockCollisionEffect();
        }
    }
}
