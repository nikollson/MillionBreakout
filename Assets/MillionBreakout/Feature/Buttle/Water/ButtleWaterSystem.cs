using System.ComponentModel;
using Tkool.ThousandBullets;
using UnityEngine;


namespace MillionBreakout
{
    public class ButtleWaterSystem : MonoBehaviour
    {

        public ThousandBulletsManager ThousandBullets;

        public float bulletRadius = 0.1f;
        public float gravity = 0.1f;
        public Vector2 rotateRange = new Vector2(0, 1);
        public Vector2 speedRange = new Vector2(1, 2);
        public Vector2 angleRange = new Vector2(-1f, 1f);

        public float WaterGetLineY { get { return ButtleSystem.Stage.Bottom; } }

        public void Start()
        {
            for (int i = 0; i < 20; i++)
            {
                MakeWater(transform.position, 5);
            }
        }


        public void Update()
        {
            ThousandBullets.RemoveIf(x => (x as WaterBullet).IsDead);
        }


        public void MakeWater(Vector3 position, int waterValue)
        {
            float angle = RandVec(angleRange) + Mathf.PI / 2;
            float speed = RandVec(speedRange);

            var velocity = new Vector2(Mathf.Cos(angle), Mathf.Sign(angle)) * speed;

            var waterBullet = new WaterBullet(
                bulletRadius, RandVec(rotateRange), waterValue,
                velocity, gravity);

            ThousandBullets.AddBullet(waterBullet, position, Quaternion.identity);   
        }

        public void AddSnow(Vector3 position, float waterValue)
        {
            int index = 0;
            float stageLeft = ButtleSystem.Stage.Left;
            float stageWidth = ButtleSystem.Stage.Width;

            if (position.x > stageLeft + stageWidth / 4)
            {
                index = 1;
            }

            if (position.x > stageLeft + stageWidth / 4 * 2)
            {
                index = 2;
            }

            if (position.x > stageLeft + stageWidth / 4 * 3)
            {
                index = 3;
            }

            var characters = ButtleSystem.Party.Characters;

            if (index >= characters.Length || characters[index].IsDead)
            {
                int livingCount = ButtleSystem.Party.LivingCharacterCount;
                foreach (var character in characters)
                {
                    character.AddSnow(waterValue / livingCount);
                }
            }
            else
            {
                characters[index].AddSnow(waterValue);
            }
        }

        public float RandVec(Vector2 vec)
        {
            return Random.Range(vec.x, vec.y);
        }
    }
}