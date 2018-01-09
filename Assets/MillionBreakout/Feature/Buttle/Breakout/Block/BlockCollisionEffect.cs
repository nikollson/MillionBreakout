
using System;
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    [Serializable]
    public class BlockCollisionEffect : IBlockCollisionEffect
    {
        public bool DoErase = false;
        public bool DoSpoit = false;
        public bool RecieveDamage = true;

        [HideInInspector]
        public int RecieveDamageMax = 5;
    }
}
