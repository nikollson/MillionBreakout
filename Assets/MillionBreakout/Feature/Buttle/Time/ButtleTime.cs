
using UnityEngine;

namespace MillionBreakout
{
    public class ButtleTime
    {
        
        public float DeltaTime
        {
            get { return Time.deltaTime * TimeScale; }
        }

        public float TimeScale { get; set; }

        public ButtleTime()
        {
            TimeScale = 1;
        }
    }
}
