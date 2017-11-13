using UnityEngine;

namespace Stool.Calc.BulletCollision
{
    public class BulletCollionBody : MonoBehaviour
    {
        [SerializeField]
        public float Radius { get; private set; }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }

        private int cnt = 0;

        void Update()
        {
            cnt++;
            if (cnt == 100)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
