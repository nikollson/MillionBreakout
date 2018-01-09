using UnityEngine;
using System.Collections;

namespace MillionBreakout
{

    public class TestBallLauncher : MonoBehaviour
    {
        public int N = 10;
        public float timeSpan = 1;
        public Vector2 VelocityX = new Vector2(0, 1);
        public Vector2 VelocityY = new Vector2(0, 1);
        public Vector2 Radius = new Vector2(0.1f, 0.2f);

        public BallCollisionEffect collisionEffect;

        private float _timeMemo = 0;

        public void Update()
        {
            _timeMemo -= Time.deltaTime;

            if (_timeMemo < 0)
            {
                _timeMemo += timeSpan;

                for (int i = 0; i < N; i++)
                {
                    var velocity = new Vector2(GetRand(VelocityX), GetRand(VelocityY));
                    var radius = GetRand(Radius);
                    var ball = new TestBall(collisionEffect, 5, radius, velocity);

                    ButtleSystem.Breakout.AddBall(ball, transform.position, Quaternion.identity);
                }
            }
        }

        private float GetRand(Vector2 vec)
        {
            return Random.Range(vec.x, vec.y);
        }
    }
}
