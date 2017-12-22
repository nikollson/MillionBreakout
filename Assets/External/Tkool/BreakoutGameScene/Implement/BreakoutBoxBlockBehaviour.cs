
using Stool.Algorithm.Geometry;
using Stool.ScriptingUtility;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class BreakoutBoxBlockBehaviour : BreakoutBlockBehaviour
    {
        public BoxCollider2D BoxCollider;
        public Rectangle Rectangle { get; private set; }

        public void Awake()
        {
            Rectangle = GetRectangle(BoxCollider);
        }

        private Rectangle GetRectangle(BoxCollider2D boxColider)
        {
            var p = boxColider.GetGlobalVertices(transform);
            var position = (p[0] + p[2]) / 2;
            var dx = p[1] - p[0];
            var dy = p[2] - p[1];
            var size = new Vector2(dx.magnitude, dy.magnitude);
            var rotation = Mathf.Atan2(dx.y, dx.x);
            return new Rectangle(position, size, rotation);
        }

        public override CircleCollisionSearcher.CheckState AreaCheck(Rectangle area, float currentWidth)
        {
            return CircleCollisionCheckFunctions.AreaCheck_Rectangle(Rectangle, area, currentWidth);
        }

        public override DistanceInfo2D CircleCheck(ICircleCollider circleCollider)
        {
            return CircleCollisionCheckFunctions.CircleCheck_Rectangle(Rectangle, circleCollider);
        }
    }
}
