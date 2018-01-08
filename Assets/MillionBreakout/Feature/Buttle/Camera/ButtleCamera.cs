using UnityEngine;
using System.Collections;

namespace MillionBreakout
{
    public class ButtleCamera : MonoBehaviour
    {
        public Camera Camera;

        void Awake()
        {
            ButtleSystem.Camera = Camera;
        }
    }
}
