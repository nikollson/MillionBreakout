using UnityEngine;


namespace MillionBreakout
{
    public class ButtlePartyUI : MonoBehaviour
    {

        public CharacterSnowInfoUI[] SnowInfoUI;
        public CharacterStatusInfoUI[] StatusInfoUI;

        private bool _started = false;

        public void Update()
        {
            var characters = ButtleSystem.Party.Characters;

            if (_started == false)
            {
                _started = true;

                for (int i = 0; i < characters.Length; i++)
                {
                    SnowInfoUI[i].Initialize(characters[i].Setting, i);
                }

                for (int i = 0; i < SnowInfoUI.Length; i++)
                {
                    bool enable = i < characters.Length;

                    SnowInfoUI[i].gameObject.SetActive(enable);
                    StatusInfoUI[i].gameObject.SetActive(enable);
                }
            }

            for (int i = 0; i < characters.Length; i++)
            {
                var c = characters[i];

                SnowInfoUI[i].gameObject.SetActive(c.IsDead == false);
                StatusInfoUI[i].gameObject.SetActive(c.IsDead == false);

                SnowInfoUI[i].SetSnow(c.SkillStock, c.GetSkillChargePer());

                StatusInfoUI[i].SetHpPer(c.HP/c.Setting.Buttle.HPMax);
                StatusInfoUI[i].SetLevelText(c.Level);
            }
        }
    }
}