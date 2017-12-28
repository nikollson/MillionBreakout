
using Stool.ScriptingUtility;
using Tkool.MilllionBullets.Collision2D;
using UnityEngine;

namespace Tkool.MilllionBullets
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MillionBulletsBoxCollider : MonoBehaviour
    {
        public int HitCount { get; private set; }
        public Searcher.BoxColliderMode mode;
        private BoxCollider2D _boxCollider;

        void Awake()
        {
            HitCount = 0;
        }
        
        public BoxData GetBox()
        {
            var vertices = BoxColliderUtility.GetGlobalVertices(GetBoxCollider(), transform);

            Vector3 center = (vertices[0] + vertices[2]) / 2;
            float width = (vertices[0] - vertices[1]).magnitude;
            float height = (vertices[0] - vertices[3]).magnitude;
            Vector3 dir = vertices[1] - vertices[0];
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
        

        public void AddHitCount(int val)
        {
            HitCount += val;
            OnAddHitCount(val);
        }

        protected virtual void OnAddHitCount(int val)
        {
            
        }
    }

    public struct BoxData
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
