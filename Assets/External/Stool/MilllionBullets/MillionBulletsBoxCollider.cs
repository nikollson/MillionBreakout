
using Stool.MilllionBullets.Collision2D;
using Stool.UnityPhysics;
using UnityEngine;

namespace Stool.MilllionBullets
{
    class MillionBulletsBoxCollider : MonoBehaviour
    {
        public BoxCollider2D BoxCollider;

        public BoxData GetBox()
        {
            var vertices = BoxCollider.GetGlobalVertices(transform);

            Vector3 center = (vertices[0] + vertices[2]) / 2;
            float width = (vertices[0] - vertices[3]).magnitude;
            float height = (vertices[0] - vertices[1]).magnitude;
            Vector3 dir = vertices[3] - vertices[0];
            float angle = Mathf.Atan2(dir.y, dir.x);

            return new BoxData(center, angle, width, height);
        }
    }
}
