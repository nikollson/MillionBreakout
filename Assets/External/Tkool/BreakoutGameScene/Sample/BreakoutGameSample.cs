using UnityEngine;
using UnityEngine.EventSystems;

namespace Tkool.BreakoutGameScene.Sample
{
    class BreakoutGameSample : MonoBehaviour
    {
        public BreakoutGameSystem BreakoutGameSystem;
        public BreakoutBlockBehaviour[] Blocks;
        public RandomBulletSetting BulletSetting;


        public void Start()
        {
            foreach (var block in Blocks)
            {
                BreakoutGameSystem.AddBlock(block);
            }
        }

        public void Update()
        {
            if (BulletSetting.IsShootTime(Time.deltaTime))
            {
                for (int i = 0; i < BulletSetting.shootNum; i++)
                {
                    var ball = new SampleBall(BulletSetting.GetRadius(),
                        BulletSetting.GetVelocity(), BulletSetting.GetTexture());
                    BreakoutGameSystem.AddBall(ball, BulletSetting.GetPosition(), BulletSetting.GetRotation());
                }
            }
        }

        [System.Serializable]
        public class RandomBulletSetting
        {
            public float shootTimeSpan = 1;
            public int shootNum = 10;

            public Vector2 Radius = new Vector2(0.1f, 0.2f);
            public Vector2 VelocityX = new Vector2(-0.1f, 0.1f);
            public Vector2 VelocityY = new Vector2(0, 0.1f);
            public Vector2 PositionX = new Vector2(-1, 1);
            public Vector2 PositionY = new Vector2(-1, 1);
            public Vector2 Rotation = new Vector2(-30, 30);

            public Texture2D Texture;

            private float time = 0;

            public bool IsShootTime(float deltaTime)
            {
                time -= deltaTime;

                if (time < 0)
                {
                    time += shootTimeSpan;
                    return true;
                }

                return false;
            }

            public float GetRadius()
            {
                return RandomRange(Radius);
            }

            public Vector2 GetVelocity()
            {
                return new Vector2(RandomRange(VelocityX), RandomRange(VelocityY));
            }

            public Vector2 GetPosition()
            {
                return new Vector2(RandomRange(PositionX), RandomRange(PositionY));
            }

            public Texture2D GetTexture()
            {
                return Texture;
            }

            public Quaternion GetRotation()
            {
                return Quaternion.Euler(new Vector3(0, 0, RandomRange(Rotation)));
            }

            private float RandomRange(Vector2 vec)
            {
                return Random.Range(vec.x, vec.y);
            }
        }
    }
}
