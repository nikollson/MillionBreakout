
using UnityEngine;

namespace StoolKit.ThousandBullets.Sample
{
    class ThousandBulletsSample : MonoBehaviour
    {
        public ThousandBulletsManager ThousandBulletManager;

        public int N = 100;
        public float deltaTime = 1;

        public NormalBulletInstance.Setting Setting;

        private float count = 0;

        public void Update()
        {
            if (count > deltaTime)
            {
                for (int i = 0; i < N; i++)
                {
                    MakeBullet(new Vector3(i * 0.5f, 0, 0), Quaternion.identity);
                }
                count -= deltaTime;
            }

            count += Time.deltaTime;
        }

        public void MakeBullet(Vector3 position, Quaternion rotation)
        {
            var behaviour = new NormalBulletInstance(Setting);
            ThousandBulletManager.AddBullet(behaviour, position, rotation);
        }
    }
}
