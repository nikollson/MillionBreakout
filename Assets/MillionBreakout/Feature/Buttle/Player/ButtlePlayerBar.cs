using UnityEngine;
using System.Collections;
using Tkool.BreakoutGameScene;


namespace MillionBreakout
{
    public class ButtlePlayerBar : BlockBehaviour
    {
        public override bool CanCollision(BreakoutBallBehaviour ball)
        {
            if (ball.Velocity.y > 0) return false;
            return true;
        }
        
        public void Update()
        {
            var touchScreen = ButtleSystem.UI.TouchScreen;

            if (touchScreen.IsTouching)
            {
                var camera = ButtleSystem.Camera;
                var touchPosition = (Vector3)touchScreen.TouchPosition;

                touchPosition.z = -camera.transform.position.z;

                var worldPosition = camera.ScreenToWorldPoint(touchPosition);
                var nextPosition = transform.position;

                nextPosition.x = worldPosition.x;

                transform.position = nextPosition;
            }
        }

        public override IBlockCollisionEffect GetCollisionEffect()
        {
            return new BlockCollisionEffect();
        }
    }
}
