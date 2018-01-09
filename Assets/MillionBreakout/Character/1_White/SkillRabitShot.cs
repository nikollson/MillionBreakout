using MillionBreakout;
using UnityEngine;

namespace MillionBreakout
{
    public class SkillRabitShot : ButtleSkillBase
    {
        public Transform[] bulletPositions;
        public int oneBulletCost = 5;

        public float radius = 0.1f;
        public Vector2 Velocity = new Vector2(0, 1);
        public Vector2 VelocityXRange = new Vector2(-0.3f, 0.3f);

        private int _level;
        private int _cost;

        private int _oneShotCost
        {
            get { return oneBulletCost * bulletPositions.Length; }
        }

        public override void StartSkill(int level, int cost)
        {
            _level = level;
            _cost = cost;
        }

        public override void OnPointerUp(Vector2 point)
        {
            foreach (var bullet in bulletPositions)
            {
                float velocityx = Random.Range(VelocityXRange.x, VelocityXRange.y);

                var velocity = Velocity + new Vector2(velocityx, 0);
                var ball = new TestBall(new BallCollisionEffect(), oneBulletCost, radius, velocity);
                Vector3 position = (Vector3) point + bullet.localPosition;

                
                ButtleSystem.Breakout.AddBall(ball, position, Quaternion.identity);
            }

            _cost -= _oneShotCost;
        }

        public override bool IsEnd()
        {
            return _cost <= 0;
        }

        public override int GetSkillCount()
        {
            return (_cost - 1) / _oneShotCost + 1;
        }
    }
}