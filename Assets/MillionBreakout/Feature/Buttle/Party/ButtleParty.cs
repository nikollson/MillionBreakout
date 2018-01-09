
using System;

namespace MillionBreakout
{
    public class ButtleParty
    {

        public Character[] Characters { get; private set; }

        public int CharacterCount { get { return Characters.Length; } }

        public int LivingCharacterCount {
            get
            {
                int ret = 0;
                foreach (var character in Characters)
                {
                    if (character.IsDead == false)
                        ret++;
                }
                return ret;
            }
        }

        public void SetParty(CharacterSetting[] characterSettings)
        {
            Characters = new Character[characterSettings.Length];

            for (int i = 0; i < Characters.Length; i++)
            {
                Characters[i] = new Character(characterSettings[i]);
            }
        }



        [Serializable]
        public class Character
        {
            public CharacterSetting Setting { get; private set; }

            public int SkillStock;

            public float Snow { get; private set; }

            public float HP = 1;

            public int Level = 1;

            public bool IsDead = false;

            public Character(CharacterSetting setting)
            {
                Setting = setting;

                HP = Setting.Buttle.HPMax;
            }

            public void AddSnow(float snowValue)
            {
                Snow += snowValue;

                float cost = NextSkillCost();

                if (Snow >= cost)
                {
                    Snow -= cost;
                    SkillStock++;
                }
            }

            public void UseSkill()
            {
                SkillStock--;
                Level++;
            }

            public float GetSkillChargePer()
            {
                return Snow / NextSkillCost();
            }

            public int NextSkillCost()
            {
                return Setting.Buttle.SkillCost.GetSkillCost(Level + SkillStock);
            }
        }
    }
}
