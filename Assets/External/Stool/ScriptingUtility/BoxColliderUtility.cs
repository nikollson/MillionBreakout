
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Stool.ScriptingUtility
{
    public static class BoxColliderUtility
    {
        public static Vector3[] GetLocalVertices(BoxCollider2D boxCollider)
        {
            var ret = new Vector3[4];

            Vector2 leftBottom = boxCollider.offset - boxCollider.size * 0.5f;
            Vector2 rightTop = boxCollider.offset + boxCollider.size * 0.5f;

            ret[0] = leftBottom;
            ret[1] = new Vector2(rightTop.x, leftBottom.y);
            ret[2] = rightTop;
            ret[3] = new Vector2(leftBottom.x, rightTop.y);

            return ret;
        }

        public static Vector3[] GetGlobalVertices(BoxCollider2D boxCollider, Transform transform)
        {
            var vertices = GetLocalVertices(boxCollider);
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = transform.TransformPoint(vertices[i]);
            }
            return vertices;
        }

        
        public static Rectangle GetRectangle(BoxCollider2D boxColider, Transform transform)
        {
            var p = GetGlobalVertices(boxColider, transform);
            var position = (p[0] + p[2]) / 2;
            var dx = p[1] - p[0];
            var dy = p[2] - p[1];
            var size = new Vector2(dx.magnitude, dy.magnitude);
            var rotation = Mathf.Atan2(dx.y, dx.x);
            return new Rectangle(position, size, rotation);
        }
    }
}
