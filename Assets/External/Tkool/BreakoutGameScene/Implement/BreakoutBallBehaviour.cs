
using Stool.Algorithm.Geometry;
using Tkool.ThousandBullets;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class BreakoutBallBehaviour : ThousandBulletBehaviour, ICircleCollider
    {
        public float Radius { get; private set; }
        public Vector2 Speed { get; private set; }
        public Texture2D Texture { get; private set; }

        public BreakoutBallBehaviour(float radius, Vector2 speed, Texture2D texture)
        {
            Radius = radius;
            Speed = speed;
            Texture = texture;
        }

        public override void OnUpdateBullet()
        {
            Transform.position = Transform.position + (Vector3)Speed * Time.deltaTime;
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

        public void OnBlockCollide(BreakoutBlockBehaviour block, DistanceInfo2D distanceInfo)
        {
            
        }
    }
}
