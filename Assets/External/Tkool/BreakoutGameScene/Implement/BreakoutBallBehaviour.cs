
using Stool.Algorithm.Geometry;
using Tkool.ThousandBullets;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    abstract class BreakoutBallBehaviour : ThousandBulletBehaviour, ICircleCollider
    {
        public bool IsDestroyed { get; private set; }
        public float Radius { get; private set; }
        public Vector2 Velocity { get; set; }

        public float DrawingRadiusExpend = 1.0f;


        public BreakoutBallBehaviour(float radius, Vector2 velocity)
        {
            Radius = radius;
            Velocity = velocity;
        }


        public void Destroy()
        {
            IsDestroyed = true;
        }

        public abstract IBallCollisionEffect GetCollisionEffect();

        public abstract void OnCollision(CircleCollisionInfo collision, IBlockCollisionEffect blockHitEffect);

        public void OnCollisionPhysicsCorrect(CircleCollisionInfo collision)
        {
            float angle = collision.AngleToCircle;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            float velocityDot = Vector2.Dot(dir, Velocity);
            Vector2 nextVelocity = Velocity - velocityDot * 2 * dir;

            Vector2 nextPosition = Transform.position + (Vector3)dir * collision.Distance * -1;

            PositionCorrect(nextPosition);
            VelocityCorrect(nextVelocity);
        }

        public virtual void PositionCorrect(Vector3 correctPosition)
        {
            Transform.position = correctPosition;
        }

        public virtual void VelocityCorrect(Vector3 correctVelocity)
        {
            Velocity = correctVelocity;
        }

        public void PositionUpdateOnFrame(float deltaTime)
        {
            Transform.position = Transform.position + (Vector3) Velocity * deltaTime;
        }

        // BulletBehaviour

        public override float GetBulletRadius()
        {
            return Radius * DrawingRadiusExpend;
        }

        // ICircleCollider

        public Vector2 GetColliderCenter()
        {
            return Transform.position;
        }

        public float GetColliderRadius()
        {
            return Radius;
        }

    }
}
