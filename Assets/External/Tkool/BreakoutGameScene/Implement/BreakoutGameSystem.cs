
using System;
using Stool.Algorithm.Geometry;
using Stool.CSharp;
using Tkool.ThousandBullets;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    public class BreakoutGameSystem : MonoBehaviour
    {
        public ThousandBulletsManager ThousandBullets;
        public CircleCollisionManager CircleCollision;
        public DictionarizedLinkedList<BreakoutBlockBehaviour> Blocks;

        public CircleCollsionSettingData CircleCollisionSetting;

        public void Awake()
        {
            CircleCollision = new CircleCollisionManager(CircleCollisionSetting.GetFormatedData());
            Blocks = new DictionarizedLinkedList<BreakoutBlockBehaviour>();
        }

        public void AddBall(BreakoutBallBehaviour ball, Vector3 position, Quaternion rotation)
        {
            ThousandBullets.AddBullet(ball, position, rotation);
            CircleCollision.AddCollider(ball);
        }

        public void AddBlock(BreakoutBlockBehaviour block)
        {
            Blocks.Add(block);
        }
        
        public void Update()
        {
            // Update

            ThousandBullets.ForeachBullets(
                x=>
                {
                    float deltaTime = ThousandBullets.GetDeltaTime();
                    (x as BreakoutBallBehaviour).PositionUpdateOnFrame(deltaTime);
                });


            // Collision

            UpdateCollision();


            // Remove

            ThousandBullets.RemoveIf(
                x => (x as BreakoutBallBehaviour).IsDestroyed,
                x => CircleCollision.RemoveCollider(x as BreakoutBallBehaviour)
            );
            Blocks.RemoveIf(x => x.IsDestroyed);


            // PrepareRender

            foreach (var block in Blocks)
            {
                block.OnRenderBase();
            }
        }


        private void UpdateCollision()
        {
            CircleCollision.UpdateColliderInfo();

            foreach (var block in Blocks)
            {
                var blockCollider = block.GetBreakoutBlockCollider();

                var circleCollisions = CircleCollision.Searcher.Check(blockCollider);


                foreach (var circleCollision in circleCollisions)
                {
                    if (circleCollision.IsHit == false)
                        continue;

                    var collision = (BreakoutBlockCollisionInfo)circleCollision;
                    var ball = collision.Collider as BreakoutBallBehaviour;

                    // Effects

                    var ballHitEffect = ball.GetCollisionEffect();
                    var blockHitEffect = block.GetCollisionEffect();

                    ball.OnCollisionPhysicsCorrect(collision);

                    ball.OnCollision(collision, blockHitEffect);

                    foreach (var gridInfo in collision.GridData)
                    {
                        block.OnCollision(
                            gridInfo.ArrayX, gridInfo.ArrayY, gridInfo.Collision, ballHitEffect);
                    }
                }
            }
        }


        [Serializable]
        public class CircleCollsionSettingData
        {
            public Vector2 areaCenter;
            public float areaWidth = 10;
            public int splitDepth = 8;

            public CircleCollisionSetting GetFormatedData()
            {
                return new CircleCollisionSetting(splitDepth, areaCenter, areaWidth);
            }
        }
    }
}
