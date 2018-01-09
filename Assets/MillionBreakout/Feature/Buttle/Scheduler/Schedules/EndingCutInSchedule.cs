using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MillionBreakout
{
    public class EndingCutInSchedule : ButtleScheduleBase
    {
        public float Time = 7;
        public string NextSceneName;

        private float _time = 0;


        public void Update()
        {
            _time += UnityEngine.Time.deltaTime;
        }
        public override void StartSchedule()
        {
            _time = 0;
            ButtleSystem.Time.TimeScale = 0;
            ButtleSystem.UI.GameClearUI.SetActive(true);
        }

        public override void EndSchedule()
        {

            SceneManager.LoadScene(NextSceneName);
        }

        public override bool IsEnd()
        {
            return _time > Time;
        }
    }
}