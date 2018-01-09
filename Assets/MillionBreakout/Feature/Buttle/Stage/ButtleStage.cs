using UnityEngine;


namespace MillionBreakout
{
    public class ButtleStage : MonoBehaviour
    {

        public float Width;
        public float Height;

        public float Top { get { return transform.position.y + Height / 2; } }
        public float Bottom { get { return transform.position.y - Height / 2; } }
        public float Left { get { return transform.position.x - Width / 2; } }
        public float Right { get { return transform.position.x - Width / 2; } }
    }
}