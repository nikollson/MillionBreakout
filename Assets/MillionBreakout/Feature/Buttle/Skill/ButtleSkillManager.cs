using UnityEngine;

namespace MillionBreakout
{
    public class ButtleSkillManager
    {

        public bool IsSkillPlaying { get { return _skill != null; } }

        private ButtleSkillBase _skill;
        private float _prevTimeScale = 1;

        public void StartSkill(ButtleSkillBase skillPrefab, int level ,int cost)
        {
            var obj = Object.Instantiate(skillPrefab);

            _skill = obj.GetComponent<ButtleSkillBase>();
            _skill.StartSkill(level, cost);

            ButtleSystem.UI.SkillUI.StartSkill(_skill);

            _prevTimeScale = ButtleSystem.Time.TimeScale;
            ButtleSystem.Time.TimeScale = 0;
        }

        public void Update()
        {
            if (_skill == null)
                return;

            if (_skill.IsEnd())
            {
                _skill.EndSkill();
                Object.Destroy(_skill.gameObject);

                _skill = null;

                ButtleSystem.UI.SkillUI.EndSkill();

                ButtleSystem.Time.TimeScale = _prevTimeScale;
            }
        }
    }
}