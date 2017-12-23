
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

        public virtual BreakoutBlockCollisionEffect MakeBlockCollisionEffect(DistanceInfo2D distanceInfo)
        {
            return new BreakoutBlockCollisionEffect(distanceInfo);
        }
        
        public virtual void RecieveCollisionEffect(BreakoutBallCollisionEffect effect)
        {
            Transform.position += (Vector3)effect.GetPositionCorrectDistance();
            Velocity = effect.GetReflectedVelocity(Velocity);

            if(effect.DoDestroy) Destroy();
        }

        public void Destroy()
        {
            OnDestroy(this);
        }
    }
}
