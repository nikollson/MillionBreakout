
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
        private CircleCollisionManager _circleCollisionManager;

        [SerializeField] private Setting _setting;

        public void Awake()
        {
            for (int i = 0; i < 100; i++)
            {
                var speed = RangeVector(_setting.speedMin, _setting.speedMax);
                var position = RangeVector(_setting.positionMin, _setting.positionMax);
                float radius = _setting.radius;
                Texture2D tex = Random.Range(0, 2) >= 1 ? _setting.texture1 : _setting.texture2;

                ThousandBulletsManager.AddBullet(new BreakoutBallBehaviour(radius, speed, tex), position,Quaternion.identity);
            }
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
