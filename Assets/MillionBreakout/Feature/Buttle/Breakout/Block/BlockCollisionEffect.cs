
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    [SerializeField]
    public class BlockCollisionEffect : IBlockCollisionEffect
    {
        public bool DoErase = false;
        public int Attack = 1;
    }
}
