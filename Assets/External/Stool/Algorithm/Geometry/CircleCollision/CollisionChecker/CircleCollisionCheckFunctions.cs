
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    static class CircleCollisionCheckFunctions
    {
        public static DistanceInfo2D CircleCheck_Rectangle(Rectangle rect, ICircleCollider collider)
        {
            var info = Distance2D.RectangleToPoint(rect, collider.GetColliderCenter());
            info.Distance -= collider.GetColliderRadius();
            return info;
        }

        public static CircleCollisionSearcher.CheckState AreaCheck_Rectangle(Rectangle rect, Rectangle area, float currentWidth)
        {
            float sinr = Mathf.Sin(rect.Rotation);
            float cosr = Mathf.Cos(rect.Rotation);

            bool isAllIn = true;
            bool isNearIn = false;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    if ((i == 0 && j != 0) || (i != 0 && j == 0))
                        continue;
                    ;
                    float ax = area.Position.x + i * area.Size.x * 0.5f - rect.Position.x;
                    float ay = area.Position.y + j * area.Size.y * 0.5f - rect.Position.y;

                    float distX = Mathf.Abs(cosr * ax + sinr * ay);
                    float distY = Mathf.Abs(-sinr * ax + cosr * ay);

                    if (distX > rect.Width * 0.5f || distY > rect.Height * 0.5f)
                    {
                        isAllIn = false;
                    }
                    if (distX <= (rect.Width + currentWidth) * 0.5f && distY <= (rect.Height + currentWidth) * 0.5f)
                    {
                        isNearIn = true;
                    }
                    if (isNearIn && isAllIn == false)
                        break;
                }
            }

            if (isAllIn)
                return CircleCollisionSearcher.CheckState.AllIn;
            if (isNearIn)
                return CircleCollisionSearcher.CheckState.NearIn;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 && j != 0) || (i != 0 && j == 0))
                        continue;

                    float bx = i * (rect.Width * 0.5f);
                    float by = j * (rect.Height * 0.5f);

                    float distX = Mathf.Abs(rect.Position.x + cosr * bx - sinr * by - area.Position.x);
                    float distY = Mathf.Abs(rect.Position.y + sinr * bx + cosr * by - area.Position.y);

                    if (distX <= (area.Height + currentWidth) * 0.5f && distY <= (area.Width + currentWidth) * 0.5f)
                    {
                        isNearIn = true;
                        break;
                    }
                }
            }

            if (isNearIn)
                return CircleCollisionSearcher.CheckState.NearIn;

            return CircleCollisionSearcher.CheckState.Out;
        }
    }
}
