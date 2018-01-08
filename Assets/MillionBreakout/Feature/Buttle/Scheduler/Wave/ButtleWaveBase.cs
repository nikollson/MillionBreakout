using UnityEngine;

namespace MillionBreakout
{

    public abstract class ButtleWaveBase : MonoBehaviour
    {

        public abstract void StartWave();

        public abstract void EndWave();

        public abstract bool IsCleared();
    }
}