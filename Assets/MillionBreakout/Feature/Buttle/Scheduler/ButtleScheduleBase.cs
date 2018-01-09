using UnityEngine;

namespace MillionBreakout
{

    public abstract class ButtleScheduleBase : MonoBehaviour
    {

        public abstract void StartSchedule();

        public abstract void EndSchedule();

        public abstract bool IsEnd();
    }
}