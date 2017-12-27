
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene.Sample
{
    class SampleGridBlock : BreakoutBlockBehaviour
    {
        public BoxCollider2D BoxCollider;

        public int Width = 4;
        public int Height = 5;

        private BreakoutBlockCollider _blockCollider;

        public void Awake()
        {
            _blockCollider = new BreakoutGridBoxCollider(Width, Height, transform, BoxCollider);
        }

        public override IBlockCollisionEffect GetCollisionEffect()
        {
            return new SampleBlockCollisionEffect();
        }

        public override void OnCollision(int arrayX, int arrayY, CircleCollisionInfo collision, IBallCollisionEffect ballHitEffect)
        {
            var effect = (SampleBallCollisionEffect) ballHitEffect;
            _blockCollider.EnableArray[arrayY, arrayX] = false;

        }

        public override BreakoutBlockCollider GetBreakoutBlockCollider()
        {
            return _blockCollider;
        }
    }
}
