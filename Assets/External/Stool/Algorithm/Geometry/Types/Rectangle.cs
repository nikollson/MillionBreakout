
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    class Rectangle
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
            get { return Size.x; }
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
