
using System.Collections.Generic;
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    class GridBlockDistanceInfo : DistanceInfo2D
    {
        public List<GridDistanceInfo> InfoList { get; private set; }

        public GridBlockDistanceInfo() : base(Mathf.Infinity, 0)
        {
            InfoList = new List<GridDistanceInfo>();
        }

        public void MergeInfo(DistanceInfo2D info, int arrayX, int arrayY)
        {
            if (Distance > info.Distance)
            {
                Distance = info.Distance;
                Angle = info.Angle;
            }
            if (info.IsHit)
            {
                InfoList.Add(new GridDistanceInfo(info, arrayX, arrayY));
            }
        }

        public class GridDistanceInfo
        {
            public DistanceInfo2D Info { get; private set; }
            public int ArrayX { get; private set; }
            public int ArrayY { get; private set; }
            public GridDistanceInfo(DistanceInfo2D info, int arrayX, int arrayY)
            {
                Info = info;
                ArrayX = arrayX;
                ArrayY = arrayY;
            }
        }
    }
}
