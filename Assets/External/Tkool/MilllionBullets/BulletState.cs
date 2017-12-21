
using UnityEngine;

namespace Tkool.MilllionBullets
{
    struct BulletState
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public float Radius;
        public int Enable;
        public int IsDead;

        public BulletState(Vector3 position, Vector3 velocity, float radius)
        {
            Position = position;
            Velocity = velocity;
            Radius = radius;
            Enable = 1;
            IsDead = 0;
        }
        public BulletState(Vector3 position, Vector3 velocity, float radius, int enable, int isDead)
        {
            Position = position;
            Velocity = velocity;
            Radius = radius;
            Enable = enable;
            IsDead = isDead;
        }
    }
}
