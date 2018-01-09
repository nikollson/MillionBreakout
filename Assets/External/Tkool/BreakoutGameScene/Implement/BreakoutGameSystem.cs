
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
            var collisionAreaSetting = CircleCollisionSetting.GetFormatedData(transform.position);
            CircleCollision = new CircleCollisionManager(collisionAreaSetting);
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
            Blocks.RemoveIf(
                x => x.IsDestroyed || x.BlockCollider.IsEnable() == false,
                x => Destroy(x.gameObject)
            );



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
                var circleCollisions = CircleCollision.Searcher.Check(block.BlockCollider);

                foreach (var circleCollision in circleCollisions)
                {
                    if (circleCollision.IsHit == false)
                        continue;

                    var collision = (BreakoutBlockCollisionInfo)circleCollision;
                    var ball = collision.Collider as BreakoutBallBehaviour;

                    if (block.CanCollision(ball) == false) continue;

                    // Effects

                    var ballHitEffect = ball.GetCollisionEffect();

                    ball.OnCollisionPhysicsCorrect(collision);


                    foreach (var gridInfo in collision.GridData)
                    {
                        block.OnCollision(
                            gridInfo.ArrayX, gridInfo.ArrayY, gridInfo.Collision, ballHitEffect);

                        var blockHitEffect = block.GetCollisionEffect(gridInfo.ArrayX, gridInfo.ArrayY);
                        ball.OnCollision(collision, blockHitEffect);
                    }
                }
            }
        }


        [Serializable]
        public class CircleCollsionSettingData
        {
            public float areaWidth = 10;
            public int splitDepth = 8;

            public CircleCollisionSetting GetFormatedData(Vector3 areaCenter)
            {
                return new CircleCollisionSetting(splitDepth, areaCenter, areaWidth);
            }
        }
    }
}
