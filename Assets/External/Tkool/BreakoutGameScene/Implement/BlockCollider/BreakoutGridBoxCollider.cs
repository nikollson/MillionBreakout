
using Stool.Algorithm.Geometry;
using Stool.ScriptingUtility;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    public class BreakoutGridBoxCollider : BreakoutBlockCollider
    {
        public Rectangle Rectangle
        {
            get
            {
                if (_rectangleUpdateFrame != Time.frameCount)
                {
                    Rectangle = BoxColliderUtility.GetRectangle(_boxCollider, _transform);
                }
                return _rectangle;
            }
            set
            {
                _rectangle = value;
                _rectangleUpdateFrame = Time.frameCount;
            }
        }

        private Rectangle _rectangle;
        private int _rectangleUpdateFrame = -1;

        private Transform _transform;
        private BoxCollider2D _boxCollider;

        public BreakoutGridBoxCollider(int Width, int Height, Transform parent, BoxCollider2D boxCollider)
            :base(new bool[Height,Width])
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    EnableArray[i, j] = true;
                }
            }
            Initalize(parent, boxCollider);
        }

        public BreakoutGridBoxCollider(bool[,] enableArray, Transform parent, BoxCollider2D boxCollider)
            : base(enableArray)
        {
            Initalize(parent, boxCollider);
        }

        private void Initalize(Transform parent, BoxCollider2D boxCollider)
        {
            _transform = parent;
            _boxCollider = boxCollider;
        }
        
        public override CircleCollisionSearcher.CheckState CircleCollision_AreaCheck(Rectangle area, float currentWidth)
        {
            if (DoCheckCollision == false)
            {
                return CircleCollisionSearcher.CheckState.Out;
            }
            return CircleCollisionCheckFunctions.AreaCheck_Rectangle(Rectangle, area, currentWidth);
        }

        public override CircleCollisionInfo CircleCollision_ColliderCheck(ICircleCollider circleCollider)
        {
            if (DoCheckCollision == false)
            {
                return new CircleCollisionInfo(circleCollider, new DistanceInfo2D(float.PositiveInfinity, 0));
            }

            var rect = Rectangle;
            var size = Rectangle.Size;
            var center = circleCollider.GetColliderCenter();
            var radius = circleCollider.GetColliderRadius();
            int arrayWidth = EnableArray.GetLength(1);
            int arrayHeight = EnableArray.GetLength(0);

            var info = new BreakoutBlockCollisionInfo(circleCollider);

            float sinr = Mathf.Sin(rect.Rotation);
            float cosr = Mathf.Cos(rect.Rotation);

            var cx = center.x - rect.Position.x + (cosr * rect.Size.x - sinr * rect.Size.y) * 0.5f;
            var cy = center.y - rect.Position.y + (sinr * rect.Size.x + cosr * rect.Size.y) * 0.5f;

            var scalex = arrayWidth / size.x;
            var scaley = arrayHeight / size.y;

            var tx = (cosr * cx + sinr * cy) * scalex;
            var ty = (-sinr * cx + cosr * cy) * scaley;

            var radx = radius * scalex;
            var rady = radius * scaley;

            int stx = Mathf.Max(0, (int)Mathf.Floor(tx - radx));
            int sty = Mathf.Max(0, (int)Mathf.Floor(ty - rady));

            int enx = Mathf.Min(arrayWidth, (int)Mathf.Ceil(tx + radx));
            int eny = Mathf.Min(arrayHeight, (int)Mathf.Ceil(ty + rady));

            for (int i = sty; i < eny; i++)
            {
                for (int j = stx; j < enx; j++)
                {
                    if (EnableArray[i, j] == false)
                        continue;

                    float dx = tx - j;
                    float dy = ty - i;

                    float distx = (dx <= 0 ? -dx : dx - 1) / scalex;
                    float disty = (dy <= 0 ? -dy : dy - 1) / scaley;

                    bool rev = distx < 0 && disty < 0;
                    if (rev)
                    {
                        if (distx < disty)
                            distx = 0;
                        else
                            disty = 0;
                    }
                    else
                    {
                        distx = Mathf.Max(0, distx);
                        disty = Mathf.Max(0, disty);
                    }

                    float distance = Mathf.Sqrt(distx * distx + disty * disty) * (rev ? -1 : 1);
                    float angle = Mathf.Atan2(
                        -disty * Mathf.Sign(dy - 0.5f) * Mathf.Sign(distance),
                        -distx * Mathf.Sign(dx - 0.5f) * Mathf.Sign(distance));

                    info.MergeInfo(new DistanceInfo2D(distance - radius, angle), j, i);
                }
            }

            return info;
        }
    }
}
