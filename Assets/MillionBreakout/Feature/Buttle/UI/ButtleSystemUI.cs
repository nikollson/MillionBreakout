using UnityEngine;


namespace MillionBreakout
{
    public class ButtleSystemUI : MonoBehaviour
    {
        public ButtleThouchScreen TouchScreen;

        public void Awake()
        {
            ButtleSystem.UI = this;
        }
    }
}