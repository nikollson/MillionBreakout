using System;
using UnityEngine;

namespace MillionBreakout
{
    [CreateAssetMenu(fileName = "CharacterSetting", menuName = "MillionBreakout/CharacterSetting")]
    public class CharacterSetting : ScriptableObject
    {

        public CharacterImages Images;
        public ButtleSetting Buttle;

        [Serializable]
        public class CharacterImages
        {
            public Sprite NormalPose;
            public Sprite MaxPose;

            public Sprite SkillBall;
        }

        [Serializable]
        public class ButtleSetting
        {
            public float HPMax = 100;
            public ButtleSkillBase Skill;
            public ButtleSkillCostSetting SkillCost;
        }
    }
}