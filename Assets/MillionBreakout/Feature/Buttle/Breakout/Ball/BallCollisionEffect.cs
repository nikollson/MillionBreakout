
using System;
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    [Serializable]
    public class BallCollisionEffect : IBallCollisionEffect
    {
        public bool DoErase = false;
        public int Attack = 1;
    }
}
