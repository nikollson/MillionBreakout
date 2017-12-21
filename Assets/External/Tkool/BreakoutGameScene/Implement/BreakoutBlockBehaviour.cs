
using Stool.Algorithm.Geometry;
using Stool.ScriptingUtility;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class BreakoutBlockBehaviour : MonoBehaviour
    {
        public BoxCollider2D Collider;

        public Rectangle GetRectangle()
        {
            var p = Collider.GetGlobalVertices(transform);
            var position = (p[0] + p[2]) / 2;
            var dx = p[1] - p[0];
            var dy = p[2] - p[1];
            var size = new Vector2(dx.magnitude, dy.magnitude);
            var rotation = Mathf.Atan2(dx.y, dx.x);
            return new Rectangle(position, size, rotation);
        }

        public void OnBallCollide(BreakoutBallBehaviour ball, DistanceInfo2D distanceInfo)
        {
            
        }
    }
}
