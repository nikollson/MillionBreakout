
using UnityEngine;

namespace Stool.MilllionBullets
{
    struct BulletState
    {
        public Vector3 Position;
        public int Enable;
        public int IsDead;

        public BulletState(Vector3 position)
        {
            Position = position;
            Enable = 1;
            IsDead = 0;
        }
        public BulletState(Vector3 position, int enable, int isDead)
        {
            Position = position;
            Enable = enable;
            IsDead = isDead;
        }
    }
}
