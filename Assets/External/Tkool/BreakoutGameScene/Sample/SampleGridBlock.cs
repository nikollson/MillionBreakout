
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene.Sample
{
    class SampleGridBlock : BreakoutBlockBehaviour
    {
        public BoxCollider2D BoxCollider2D;

        public int Width = 4;
        public int Height = 5;
        public bool DoErase = false;
        public bool CantErase = false;

        private BreakoutBlockCollider _blockCollider;

        public void Awake()
        {
            _blockCollider = new BreakoutGridBoxCollider(Width, Height, transform, BoxCollider2D);
        }

        public override IBlockCollisionEffect GetCollisionEffect()
        {
            return new SampleBlockCollisionEffect(DoErase);
        }

        public override void OnCollision(int arrayX, int arrayY, CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect)
        {
            var effect = (SampleBallCollisionEffect) ballHitEffect;

            if (effect.DoErase == true && CantErase == false)
            {
                _blockCollider.EnableArray[arrayY, arrayX] = false;
            }
        }

        public override BreakoutBlockCollider GetBreakoutBlockCollider()
        {
            return _blockCollider;
        }
    }
}
