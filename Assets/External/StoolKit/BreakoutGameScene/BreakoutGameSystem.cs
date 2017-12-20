
using System;
using Stool.Algorithm.Geometry;
using StoolKit.ThousandBullets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StoolKit.BreakoutGameScene
{
    class BreakoutGameSystem : MonoBehaviour
    {
        public ThousandBulletsManager ThousandBulletsManager;
        public CircleCollisionManager CircleCollisionManager { get; private set; }

        public Vector2 areaCenter;
        public float areaSize;
        public int areaDepth;

        [SerializeField] private Setting _setting;

        public void Awake()
        {
            var circleColliderSetting = new CircleCollisionSetting(areaDepth, areaCenter, areaSize);

            CircleCollisionManager = new CircleCollisionManager(circleColliderSetting);

            for (int i = 0; i < 10; i++)
            {
                var speed = RangeVector(_setting.speedMin, _setting.speedMax);
                var position = RangeVector(_setting.positionMin, _setting.positionMax);
                float radius = _setting.radius;
                Texture2D tex = Random.Range(0, 2) >= 1 ? _setting.texture1 : _setting.texture2;

                if (i == 0)
                {
                    radius = 1.9f;
                }

                var ball = new BreakoutBallBehaviour(radius, speed, tex);

                ThousandBulletsManager.AddBullet(ball, position,Quaternion.identity);
                CircleCollisionManager.AddCollider(ball);
            }

            CircleCollisionManager.UpdateColliderInfo();
            Debug.Log(CircleCollisionManager.Dump());
        }

        private Vector2 RangeVector(Vector2 min, Vector2 max)
        {
            return new Vector2(Random.Range(min.x,max.x), Random.Range(min.y,max.y));
        }

        [Serializable]
        public class Setting
        {
            public float radius;

            public Vector2 positionMin;
            public Vector2 positionMax;

            public Vector2 speedMin;
            public Vector2 speedMax;

            public Texture2D texture1;
            public Texture2D texture2;
        }
    }
}
