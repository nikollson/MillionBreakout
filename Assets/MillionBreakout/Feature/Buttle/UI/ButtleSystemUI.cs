using UnityEngine;


namespace MillionBreakout
{
    public class ButtleSystemUI : MonoBehaviour
    {
        public ButtleThouchScreen TouchScreen;
        public ButtleSkillUI SkillUI;
        public GameObject GameClearUI;

        public void Awake()
        {
            ButtleSystem.UI = this;
        }
    }
}