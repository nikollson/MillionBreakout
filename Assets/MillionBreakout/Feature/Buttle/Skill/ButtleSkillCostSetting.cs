using UnityEngine;

namespace MillionBreakout
{
    [CreateAssetMenu(fileName = "SkillCostSetting", menuName = "MillionBreakout/SkillCostSetting")]
    public class ButtleSkillCostSetting : ScriptableObject
    {
        public float Level1Cost = 100;
        public float PlusCost = 20;
        public float MultipulCost = 1.05f;

        public int GetSkillCost(int level)
        {
            return (int) (Level1Cost + PlusCost * (level - 1) * Mathf.Pow(MultipulCost, level - 1));
        }
    }
}