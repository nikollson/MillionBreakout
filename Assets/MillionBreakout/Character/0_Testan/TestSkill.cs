using MillionBreakout;
using UnityEngine;

public class TestSkill : ButtleSkillBase
{
    public int TouchCount = 3;

    private int _count = 0;


    public override void StartSkill(int level, int cost)
    {

    }

    public override void OnPointerDown(Vector2 point)
    {
        _count++;
    }

    public override bool IsEnd()
    {
        return _count >= TouchCount;
    }

    public override int GetSkillCount()
    {
        return TouchCount-_count;
    }
}
