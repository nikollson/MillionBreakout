
using System.Collections.Generic;
using Stool.CSharp;
using UnityEngine;

namespace Tkool.ThousandBullets.Sample
{
    class ThousandBulletsSample : MonoBehaviour
    {
        public ThousandBulletsManager ThousandBulletManager;

        public int N = 100;
        public float deltaTime = 1;

        public NormalBulletInstance.Setting Setting;

        private LinkedList<NormalBulletInstance> bullets;
        private float count = 0;

        void Awake()
        {
            bullets = new LinkedList<NormalBulletInstance>();
        }

        public void Update()
        {
            if (count > deltaTime)
            {
                for (int i = 0; i < N; i++)
                {
                    var bullet = MakeBullet(new Vector3(i * 0.5f, 0, 0), Quaternion.identity);
                    bullets.AddLast(bullet);
                }
                count -= deltaTime;
            }

            count += Time.deltaTime;

            bullets.RemoveNodeIf(x => x.IsDead, x => ThousandBulletManager.Remove(x));
        }

        public NormalBulletInstance MakeBullet(Vector3 position, Quaternion rotation)
        {
            var behaviour = new NormalBulletInstance(Setting);
            ThousandBulletManager.AddBullet(behaviour, position, rotation);

            return behaviour;
        }
    }
}
