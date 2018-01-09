using Tkool.ThousandBullets;
using UnityEngine;


namespace MillionBreakout
{
    public class WaterBullet : ThousandBulletBehaviour
    {
        public float Radius { get; private set; }

        public float RotateSpeed { get; private set; }

        public float WaterValue { get; private set; }

        public Vector2 Velocity { get; private set; }

        public float Gravity { get; private set; }

        public bool IsDead { get; private set; }

        public WaterBullet(float radius, float rotateSpeed, float waterValue, Vector2 velocity, float gravity)
        {
            Radius = radius;
            RotateSpeed = rotateSpeed;
            WaterValue = waterValue;
            Velocity = velocity;
            Gravity = gravity;
        }

        public override void OnUpdateBullet(float deltaTime)
        {
            if(IsDead)return;

            Transform.position = Transform.position + (Vector3)Velocity * deltaTime;
            Transform.Rotate(new Vector3(0, 0, RotateSpeed * deltaTime));

            Velocity = new Vector2(Velocity.x, Velocity.y - Gravity * deltaTime);

            if (Transform.position.y < ButtleSystem.WaterSystem.WaterGetLineY)
            {
                ButtleSystem.WaterSystem.AddSnow(Transform.position, WaterValue);
                IsDead = true;
            }
        }

        public override float GetBulletRadius()
        {
            return Radius;
        }
    }
}