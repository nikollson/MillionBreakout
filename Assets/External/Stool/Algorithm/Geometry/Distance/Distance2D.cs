
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    static class Distance2D
    {
        public static DistanceInfo2D RectangleToPoint(Rectangle a, Vector2 b)
        {
            float sinr = Mathf.Sin(a.Rotation);
            float cosr = Mathf.Cos(a.Rotation);
            
            float rx = b.x - a.Position.x;
            float ry = b.y - a.Position.y;

            float x = cosr * rx + sinr * ry;
            float y = -sinr * rx + cosr * ry;

            float distx = Mathf.Abs(x) - a.Width * 0.5f;
            float disty = Mathf.Abs(y) - a.Height * 0.5f;

            bool rev = distx < 0 && disty < 0;
            if (distx < 0 && distx < disty) distx = 0;
            if (disty < 0 && disty < distx) disty = 0;

            float distance = Mathf.Sqrt(distx * distx + disty * disty) * (rev ? -1 : 1);
            float angle = Mathf.Atan2(-disty * Mathf.Sign(y), -distx * Mathf.Sign(x));

            return new DistanceInfo2D(distance, angle);
        }
    }
}
