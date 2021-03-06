﻿
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    public class DistanceInfo2D
    {
        public bool IsHit { get { return Distance <= 0; } }
        public float Distance;
        public float Angle;

        public DistanceInfo2D(float distance, float angle)
        {
            Distance = distance;
            Angle = angle;
        }
    }
}
