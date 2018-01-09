
using Tkool.BreakoutGameScene;
using Tkool.ThousandBullets;
using UnityEngine;

namespace MillionBreakout
{
    public class ButtleSystemObject : MonoBehaviour
    {
        [SerializeField] private BreakoutGameSystem _breakoutGameSystem;
        [SerializeField] private ButtleWaterSystem _waterSystem;
        [SerializeField] private ButtleStage _buttleStage;

        public void Awake()
        {
            ButtleSystem.Breakout = _breakoutGameSystem;
            ButtleSystem.WaterSystem = _waterSystem;
            ButtleSystem.Stage = _buttleStage;
            ButtleSystem.Time = new ButtleTime();
            ButtleSystem.SkillManager = new ButtleSkillManager();
            ButtleSystem.Party = new ButtleParty();

            ButtleSystem.Breakout.ThousandBullets.SetDeltaTimeFunction(GetDeltaTime);
            ButtleSystem.WaterSystem.ThousandBullets.SetDeltaTimeFunction(GetDeltaTime);
        }

        public void Update()
        {
            ButtleSystem.SkillManager.Update();
        }

        private float GetDeltaTime()
        {
            return ButtleSystem.Time.DeltaTime;
        }
    }
}
