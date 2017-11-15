
using UnityEngine;

namespace Stool.MilllionBullets.Collision2D
{
    struct BoxData
    {
        public Vector3 Center;
        public float Angle;
        public float Width;
        public float Height;

        public BoxData(Vector3 center, float angle, float width, float height)
        {
            Center = center;
            Angle = angle;
            Width = width;
            Height = height;
        }
    }
}
