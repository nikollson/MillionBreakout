using UnityEngine;


namespace MillionBreakout
{
    public class ButtleScheduleController : MonoBehaviour
    {

        public ButtleScheduleBase[] Schedules;

        public int ScheduleNum { get; private set; }
        public int ScheduleCount { get { return Schedules.Length; } }

        public void Start()
        {
            ScheduleNum = 0;

            Update();
        }

        public void Update()
        {
            while (true)
            {
                if (ScheduleNum >= ScheduleCount) break;

                if (Schedules[ScheduleNum].IsEnd() == false) break;

                Schedules[ScheduleNum].EndSchedule();

                ScheduleNum++;

                if (ScheduleNum < ScheduleCount)
                {
                    Schedules[ScheduleNum].StartSchedule();
                }
            }
        }
    }
}