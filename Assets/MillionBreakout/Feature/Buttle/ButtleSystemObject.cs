
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    public class ButtleSystemObject : MonoBehaviour
    {
        [SerializeField] BreakoutGameSystem _breakoutGameSystem;

        public void Awake()
        {
            ButtleSystem.Breakout = _breakoutGameSystem;
            ButtleSystem.Time = new ButtleTime();


        }
    }
}
