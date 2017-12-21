
using UnityEngine;

namespace Tkool.MilllionBullets.Collision2D
{
    class BulletsCollision
    {
        public CommonData Data { get; private set; }
        public BufferAdder Adder { get; private set; }
        public Searcher Searcher { get; private set; }

        public BulletsCollision(ComputeShader computeShader)
        {
            Data = new CommonData();
            Adder = new BufferAdder(Data);
            Searcher = new Searcher(Data, computeShader);
        }

        public void Release()
        {
            Searcher.Release();
        }
    }
}
