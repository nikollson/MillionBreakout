
using System;
using UnityEngine;

namespace MillionBreakout
{

    public abstract class ButtleSkillBase : MonoBehaviour
    {
        public enum ControllModeType
        {
            Tap, Swipe
        }

        public ControllModeType ControllMode;

        public abstract void StartSkill(int level, int cost);

        public virtual void OnPointerDown(Vector2 point)
        {
            
        }

        public virtual void OnPointerUp(Vector2 point)
        {
            
        }

        public virtual void OnPointerMove(Vector2 point)
        {
            
        }

        public virtual void EndSkill()
        {
            
        }

        public abstract bool IsEnd();

        public abstract int GetSkillCount();
    }
}
