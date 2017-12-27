
using Stool.Algorithm.Geometry;
using Stool.ScriptingUtility;
using UnityEngine;

namespace Tkool.BreakoutGameScene_Old
{
    class BreakoutBoxBlockBehaviour : BreakoutBlockBehaviour
    {
        public BoxCollider2D BoxCollider;

        public Rectangle Rectangle
        {
            get
            {
                if (_rectangleMadeFrame != Time.frameCount)
                {
                    _rectangle = BoxColliderUtility.GetRectangle(BoxCollider, transform);
                    _rectangleMadeFrame = Time.frameCount;
                }
                return _rectangle;
            }
        }

        private Rectangle _rectangle;
        private int _rectangleMadeFrame = -1;



        public override CircleCollisionSearcher.CheckState CircleCollision_AreaCheck(Rectangle area, float currentWidth)
        {
            return CircleCollisionCheckFunctions.AreaCheck_Rectangle(Rectangle, area, currentWidth);
        }

        public override CircleCollisionInfo CircleCollision_ColliderCheck(ICircleCollider circleCollider)
        {
            return CircleCollisionCheckFunctions.CircleCheck_Rectangle(Rectangle, circleCollider);
        }
    }
}
