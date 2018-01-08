
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    [SerializeField]
    public class BallCollisionEffect : IBallCollisionEffect
    {
        public bool DoErase = false;
        public int Attack = 1;
    }
}
