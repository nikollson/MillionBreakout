﻿
using Stool.MilllionBullets.Collision2D;
using Stool.UnityPhysics;
using UnityEngine;

namespace Stool.MilllionBullets
{
    [RequireComponent(typeof(BoxCollider2D))]
    class MillionBulletsBoxCollider : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;
        
        public BoxData GetBox()
        {
            var vertices = GetBoxCollider().GetGlobalVertices(transform);

            Vector3 center = (vertices[0] + vertices[2]) / 2;
            float width = (vertices[0] - vertices[3]).magnitude;
            float height = (vertices[0] - vertices[1]).magnitude;
            Vector3 dir = vertices[3] - vertices[0];
            float angle = Mathf.Atan2(dir.y, dir.x);

            return new BoxData(center, angle, width, height);
        }

        public BoxCollider2D GetBoxCollider()
        {
            if (_boxCollider == null)
            {
                _boxCollider = GetComponent<BoxCollider2D>();
            }
            return _boxCollider;
        }
    }

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