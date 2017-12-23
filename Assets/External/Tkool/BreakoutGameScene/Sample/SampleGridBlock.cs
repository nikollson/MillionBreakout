
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene.Sample
{
    class SampleGridBlock : BreakoutGridBlockBehaviour
    {
        public int Width = 5;
        public int Height = 5;

        private BlockData[,] _blockData;

        public void Awake()
        {
            _blockData = new BlockData[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _blockData[i,j] = new BlockData();
                }
            }
        }

        public override IBreakoutGridBlockData[,] GetBlockArray()
        {
            return _blockData;
        }


        public override void RecieveCollisionEffectGrid(BreakoutBlockCollisionEffect effect, GridBlockDistanceInfo distanceInfo)
        {
            foreach (var info in distanceInfo.InfoList)
            {
                _blockData[info.ArrayY, info.ArrayX].IsLiving = false;
            }
        }

        class BlockData : IBreakoutGridBlockData
        {
            public bool IsLiving = true;

            public bool IsEnable()
            {
                return IsLiving;
            }
        }
    }
}
