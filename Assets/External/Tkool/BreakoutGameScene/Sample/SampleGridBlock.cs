
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene.Sample
{
    public class SampleGridBlock : BreakoutBlockBehaviour
    {
        public bool DoErase = false;
        public bool CantErase = false;

        public override IBlockCollisionEffect GetCollisionEffect(int arrrayX, int arrayY)
        {
            return new SampleBlockCollisionEffect(DoErase);
        }

        public override void OnCollision(int arrayX, int arrayY, CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect)
        {
            var effect = (SampleBallCollisionEffect) ballHitEffect;

            if (effect.DoErase == true && CantErase == false)
            {
                BlockCollider.EnableArray[arrayY, arrayX] = false;
            }
        }

        public void Update()
        {
            if (BlockCollider.IsEnable() == false)
            {
                Destroy();
            }
        }
    }
}
