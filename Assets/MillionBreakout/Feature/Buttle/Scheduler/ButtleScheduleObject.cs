using UnityEngine;


namespace MillionBreakout
{
    public class ButtleScheduleObject : MonoBehaviour
    {

        public ButtleWaveBase[] Waves;

        public int WaveNum { get; private set; }
        public int WaveCount { get { return Waves.Length; } }

        public void Start()
        {
            WaveNum = 0;
            Waves[WaveNum].StartWave();
        }

        public void Update()
        {
            if (WaveNum < WaveCount)
            {
                if (Waves[WaveNum].IsCleared())
                {
                    Waves[WaveNum].EndWave();

                    WaveNum++;

                    if (WaveNum < Waves.Length)
                    {
                        Waves[WaveNum].StartWave();
                    }
                }
            }

        }

        public bool IsCleared()
        {
            return Waves[Waves.Length - 1].IsCleared();
        }
    }
}