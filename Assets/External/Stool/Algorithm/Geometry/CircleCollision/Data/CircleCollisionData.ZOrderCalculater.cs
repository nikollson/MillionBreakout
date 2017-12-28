
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    public partial class CircleCollisionData
    {
        static class ZOrderCalculater
        {
            public static int GetZOrder(ICircleCollider collider, CircleCollisionSetting setting)
            {
                Rectangle rect = setting.GetAreaRectangle();
                int zorder = 0;

                for (int i = 0; i < setting.Depth; i++)
                {
                    bool isCircleBigger = rect.Width <= collider.GetColliderRadius() * 2;
                    bool isDepthMax = i == setting.Depth - 1;

                    if (isCircleBigger || isDepthMax)
                        break;

                    Vector2 toCircle = collider.GetColliderCenter() - rect.Position;
                    Vector2 nextCenter = rect.Position - rect.Size / 4;

                    zorder *= 4;
                    zorder += 1;
                    if (toCircle.x >= 0)
                    {
                        zorder += 1;
                        nextCenter.x += rect.Width / 2;
                    }
                    if (toCircle.y >= 0)
                    {
                        zorder += 2;
                        nextCenter.y += rect.Height / 2;
                    }

                    rect.Position = nextCenter;
                    rect.Size /= 2;
                }

                return zorder;
            }
        }
    }
}
