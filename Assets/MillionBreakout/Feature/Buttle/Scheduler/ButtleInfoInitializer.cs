using System;
using UnityEngine;


namespace MillionBreakout
{
    public class ButtleInfoInitializer : MonoBehaviour
    {

        public Setting[] Settings;

        public void Start()
        {
            var characters = new CharacterSetting[Settings.Length];

            for (int i = 0; i < Settings.Length; i++)
            {
                characters[i] = Settings[i].Character;
            }

            ButtleSystem.Party.SetParty(characters);

            for (int i = 0; i < Settings.Length; i++)
            {
                ButtleSystem.Party.Characters[i].Level = Settings[i].StartLevel;
                ButtleSystem.Party.Characters[i].SkillStock = Settings[i].StartStock;
            }
        }


        [Serializable]

        public class Setting
        {
            public CharacterSetting Character;
            public int StartLevel = 1;
            public int StartStock = 0;
        }
    }
}