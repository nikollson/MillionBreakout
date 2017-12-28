
using System.Collections.Generic;
using UnityEngine;

namespace Tkool.MilllionBullets.Collision2D
{
    public class CommonData
    {
        public List<ComputeBuffer> BulletsBuffers { get; private set; }

        public CommonData()
        {
            BulletsBuffers = new List<ComputeBuffer>();
        }
    }
}
