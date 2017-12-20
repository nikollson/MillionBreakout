
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    class CircleCollisionSetting
    {
        public int Depth { get; private set; }
        public int MaxZOrder { get; private set; }

        public Vector2 AreaCenter { get; private set; }
        public float AreaWidth { get; private set; }

        public CircleCollisionSetting(int depth, Vector2 areaCenter, float areaWidth)
        {
            AreaCenter = areaCenter;
            AreaWidth = areaWidth;

            Depth = depth;

            int zorderCount = 0;
            int scale = 1;
            for (int i = 0; i < depth; i++)
            {
                zorderCount += scale;
                scale *= 4;
            }
            MaxZOrder = zorderCount;
        }

        public Rectangle GetAreaRectangle()
        {
            return new Rectangle(AreaCenter, new Vector2(AreaWidth, AreaWidth), 0);
        }
    }
}
