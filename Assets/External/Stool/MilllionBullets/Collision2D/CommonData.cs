
using System.Collections.Generic;
using UnityEngine;

namespace Stool.MilllionBullets.Collision2D
{
    class CommonData
    {
        public List<ComputeBuffer> BulletsBuffers { get; private set; }

        public CommonData()
        {
            BulletsBuffers = new List<ComputeBuffer>();
        }
    }
}
