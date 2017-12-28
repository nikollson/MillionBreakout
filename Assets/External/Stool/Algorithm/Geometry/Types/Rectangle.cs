
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    public struct Rectangle
    {
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;

        public float Width {
            get { return Size.x; }
            set { Size.x = value; }
        }

        public float Height
        {
            get { return Size.y; }
            set { Size.y = value; }
        }

        public Rectangle(Vector2 position, Vector2 size, float rotation)
        {
            Position = position;
            Size = size;
            Rotation = rotation;
        }
    }
}
