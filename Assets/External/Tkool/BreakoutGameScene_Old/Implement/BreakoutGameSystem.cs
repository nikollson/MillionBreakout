
using System;
using Stool.Algorithm.Geometry;
using Stool.CSharp;
using Tkool.ThousandBullets;
using UnityEngine;

namespace Tkool.BreakoutGameScene_Old
{
    class BreakoutGameSystem : MonoBehaviour
    {
        public ThousandBulletsManager ThousandBulletsManager;
        public CircleCollisionManager CircleCollisionManager { get; private set; }
        public DictionarizedLinkedList<BreakoutBlockBehaviour> BlocksManager { get; private set; }

        [SerializeField] private CircleCollsionSettingData _circleCollisionData;
        
        public void Awake()
        {
            CircleCollisionManager = new CircleCollisionManager(_circleCollisionData.GetFormatedData());
            BlocksManager = new DictionarizedLinkedList<BreakoutBlockBehaviour>();
        }

        public void AddBall(BreakoutBallBehaviour ball, Vector3 position, Quaternion rotation)
        {
            ThousandBulletsManager.AddBullet(ball, position, rotation);
            CircleCollisionManager.AddCollider(ball);

            ball.OnDestroy += RemoveBall;
        }

        private void RemoveBall(BreakoutBallBehaviour ball)
        {
            ThousandBulletsManager.Remove(ball);
            CircleCollisionManager.RemoveCollider(ball);
        }

        public void AddBlock(BreakoutBlockBehaviour block)
        {
            BlocksManager.Add(block);

            block.OnDestroy += RemoveBlock;
        }

        private void RemoveBlock(BreakoutBlockBehaviour block)
        {
            BlocksManager.Remove(block);
        }

        public void Update()
        {
            CircleCollisionManager.UpdateColliderInfo();

            foreach (var block in BlocksManager)
            {
                var result = CircleCollisionManager.Searcher.Check(block);
                
                foreach (var collision in result)
                {
                    var ball = (BreakoutBallBehaviour) collision.Collider;

                    if (block.CanCollision(ball) == false) continue;

                    var distanceInfo = new DistanceInfo2D(collision.Distance, collision.AngleToOpponent);

                    var blockEffect = ball.MakeBlockCollisionEffect(distanceInfo);
                    var ballEffect = block.MakeBallCollisionEffect(distanceInfo);

                    ball.RecieveCollisionEffect(ballEffect);
                    block.RecieveCollisionEffect(blockEffect);
                }
            }
        }

        [Serializable]
        class CircleCollsionSettingData
        {
            public Vector2 areaCenter;
            public float areaWidth = 10;
            public int splitDepth = 8;

            public CircleCollisionSetting GetFormatedData()
            {
                return new CircleCollisionSetting(splitDepth,areaCenter,areaWidth);
            }
        }
    }
}
