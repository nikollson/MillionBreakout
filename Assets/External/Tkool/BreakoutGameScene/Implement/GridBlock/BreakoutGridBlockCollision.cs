
using System.Collections.Generic;
using Stool.Algorithm.Geometry;
using UnityEngine;

namespace Tkool.BreakoutGameScene
{
    static class BreakoutGridBlockCollision
    {

        public static DistanceInfo2D CheckHitCircle(Rectangle rect, bool[,] enableArray, ICircleCollider circleCollider)
        {
            var size = rect.Size;
            var center = circleCollider.GetColliderCenter();
            var radius = circleCollider.GetColliderRadius();
            int arrayWidth = enableArray.GetLength(1);
            int arrayHeight = enableArray.GetLength(0);

            var info = new GridBlockDistanceInfo();

            float sinr = Mathf.Sin(rect.Rotation);
            float cosr = Mathf.Cos(rect.Rotation);

            var cx = center.x - rect.Position.x + (cosr * rect.Size.x - sinr * rect.Size.y) * 0.5f;
            var cy = center.y - rect.Position.y + (sinr * rect.Size.x + cosr * rect.Size.y) * 0.5f;

            var scalex = arrayWidth / size.x;
            var scaley = arrayHeight / size.y;

            var tx = (cosr * cx + sinr * cy) * scalex;
            var ty = (-sinr * cx + cosr * cy) * scaley;

            var radx = radius * scalex;
            var rady = radius * scaley;

            int stx = Mathf.Max(0, (int)Mathf.Floor(tx - radx));
            int sty = Mathf.Max(0, (int)Mathf.Floor(ty - rady));

            int enx = Mathf.Min(arrayWidth, (int)Mathf.Ceil(tx + radx));
            int eny = Mathf.Min(arrayHeight, (int)Mathf.Ceil(ty + rady));

            for (int i = sty; i < eny; i++)
            {
                for (int j = stx; j < enx; j++)
                {
                    if(enableArray[i,j]==false)
                        continue;

                    float dx = tx - j;
                    float dy = ty - i;

                    float distx = (dx <= 0 ? -dx : dx - 1) / scalex;
                    float disty = (dy <= 0 ? -dy : dy - 1) / scaley;

                    bool rev = distx < 0 && disty < 0;
                    if (rev)
                    {
                        if (distx < disty)
                            distx = 0;
                        else
                            disty = 0;
                    }
                    else
                    {
                        distx = Mathf.Max(0, distx);
                        disty = Mathf.Max(0, disty);
                    }

                    float distance = Mathf.Sqrt(distx * distx + disty * disty) * (rev ? -1 : 1);
                    float angle = Mathf.Atan2(
                        -disty * Mathf.Sign(dy - 0.5f) * Mathf.Sign(distance),
                        -distx * Mathf.Sign(dx - 0.5f) * Mathf.Sign(distance));

                    info.MergeInfo(new DistanceInfo2D(distance - radius, angle), j, i);
                }
            }

            return info;
        }

        public class GridBlockDistanceInfo : DistanceInfo2D
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
}
