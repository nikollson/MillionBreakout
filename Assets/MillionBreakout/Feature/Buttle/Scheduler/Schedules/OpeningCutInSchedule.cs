using UnityEngine;

namespace MillionBreakout
{
    public class OpeningCutInSchedule : ButtleScheduleBase
    {

        public float ChangeTime = 1;

        private float _time = 0;

        public override void StartSchedule()
        {

        }

        public void Update()
        {
            _time += ButtleSystem.Time.DeltaTime;
        }

        public override void EndSchedule()
        {

        }

        public override bool IsEnd()
        {
            return _time > ChangeTime;
        }
    }
}