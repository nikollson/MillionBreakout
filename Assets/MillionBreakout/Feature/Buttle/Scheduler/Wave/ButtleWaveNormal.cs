
using System.Runtime.InteropServices;

namespace MillionBreakout
{
    public class ButtleWaveNormal : ButtleWaveBase
    {
        public enum ClearStateEnum
        {
            Time, AllBroken
        }


        public ClearStateEnum ClearState;

        public float ClearTime = 10;

        public BlockBehaviour[] BlockList;


        private float _timeCount = 0;


        public void Awake()
        {
            foreach (var block in BlockList)
            {
                block.gameObject.SetActive(false);
            }
        }

        public void Update()
        {
            _timeCount += ButtleSystem.Time.DeltaTime;
        }

        public override void StartWave()
        {
            foreach (var block in BlockList)
            {
                block.gameObject.SetActive(true);
            }
        }

        public override void EndWave()
        {

        }

        public override bool IsCleared()
        {
            if (ClearState == ClearStateEnum.AllBroken)
            {
                bool living = false;

                foreach (var blockBehaviour in BlockList)
                {
                    if (blockBehaviour == null || blockBehaviour.gameObject == null)
                        continue;
                    ;
                    living = true;
                }

                return living == false;
            }

            if (ClearState == ClearStateEnum.Time)
            {
                return _timeCount > ClearTime;
            }

            return false;
        }
    }
}
