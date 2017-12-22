
using UnityEngine;

namespace Stool.ScriptingUtility
{
    static class ColliderUtility
    {
        public static Vector3[] GetLocalVertices(this BoxCollider2D boxCollider)
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

        public static Vector3[] GetGlobalVertices(this BoxCollider2D boxCollider, Transform transform)
        {
            var vertices = GetLocalVertices(boxCollider);
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = transform.TransformPoint(vertices[i]);
            }
            return vertices;
        }
    }
}
