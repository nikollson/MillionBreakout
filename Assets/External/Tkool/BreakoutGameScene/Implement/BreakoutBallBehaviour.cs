
using System;
using Stool.Algorithm.Geometry;
using Tkool.ThousandBullets;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class BreakoutBallBehaviour : ThousandBulletBehaviour, ICircleCollider
    {
        public float Radius { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Texture2D Texture { get; private set; }

        public Action<BreakoutBallBehaviour> OnDestroy;

        public BreakoutBallBehaviour(float radius, Vector2 velocity, Texture2D texture)
        {
            Radius = radius;
            Velocity = velocity;
            Texture = texture;
        }

        public override void OnUpdateBullet()
        {
            Transform.position = Transform.position + (Vector3)Velocity * Time.deltaTime;
        }

        public override Texture2D GetInitialBulletTexture()
        {
            return Texture;
        }

        public override float GetBulletRadius()
        {
            return Radius;
        }

        public Vector2 GetColliderCenter()
        {
            return Transform.position;
        }

        public float GetColliderRadius()
        {
            return Radius;
        }

        public virtual void OnBlockCollide(BreakoutBlockBehaviour block, DistanceInfo2D distanceInfo)
        {
            ReflectCollision(distanceInfo);
        }

        public void ReflectCollision(DistanceInfo2D distance)
        {
            Vector2 reflectDir = new Vector2(-Mathf.Cos(distance.Angle), -Mathf.Sin(distance.Angle));

            Transform.position += (Vector3) (distance.Distance * reflectDir * -1);

            float velocityDot = Mathf.Min(0, Vector3.Dot(reflectDir, Velocity));
            Velocity += velocityDot * reflectDir * -2;
        }

        public void Destroy()
        {
            OnDestroy(this);
        }
    }
}
