
using UnityEngine;

namespace Stool.MilllionBullets.Implements
{
    class EmptyIndexQueue
    {
        private int _length;
        private ComputeShader _emptyComputeShader;

        public EmptyIndexQueue(ComputeShader emptyComputeShader, int length)
        {
            _length = length;
            _emptyComputeShader = emptyComputeShader;
        }

        public int[] GetEmptyIndices(int num)
        {
            var ret = new int[num];
            for (int i = 0; i < num; i++) ret[i] = i;
            return ret;
        }
    }
}
