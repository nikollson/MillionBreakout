
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
	partial class CircleCollisionChecker
	{
		private CircleCollisionData _data;

		private enum CheckState
		{
			AllIn,
			NearIn,
			Out
		}

		public CircleCollisionChecker(CircleCollisionData data)
		{
			_data = data;
		}

		public List<ICircleCollider> CheckRectangle(Rectangle rect)
		{
			var ret = new List<ICircleCollider>();

			RecursiveCheck(
				(x, y) => CheckRectangle_Area(rect, x, y),
				x => CheckRectangle_Collider(rect, x),
				ret, 0, _data.Setting.GetAreaRectangle(), CheckState.NearIn);

			return ret;
		}

		private DistanceInfo2D CheckRectangle_Collider(Rectangle rect, ICircleCollider collider)
		{
		    var info = Distance2D.RectangleToPoint(rect, collider.GetColliderCenter());
		    info.Distance -= collider.GetColliderRadius() * 0.5f;
			return info;
		}


		private CheckState CheckRectangle_Area(Rectangle rect, Rectangle area, float currentWidth)
		{
			float sinr = Mathf.Sin(rect.Rotation);
			float cosr = Mathf.Cos(rect.Rotation);

			bool isAllIn = true;
			bool isNearIn = false;

			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j < 1; j++)
				{
					if ((i == 0 && j != 0) || (i != 0 && j == 0)) continue;
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
				    if (isNearIn && isAllIn == false) break;
				}
			}

			if (isAllIn) return CheckState.AllIn;
			if (isNearIn) return CheckState.NearIn;

			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if ((i == 0 && j != 0) || (i != 0 && j == 0)) continue;

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

			if (isNearIn) return CheckState.NearIn;

			return CheckState.Out;
		}

		private void RecursiveCheck(
			Func<Rectangle, float, CheckState> areaFunc,
			Func<ICircleCollider, DistanceInfo2D> colliderFunc,
			List<ICircleCollider> result,
		   int zorder, Rectangle area, CheckState parentState)
		{

			if(zorder >= _data.Setting.MaxZOrder)return;

			CheckState currentState = parentState;
			if (parentState == CheckState.NearIn)
			{
				currentState = areaFunc(area, area.Width);
			}

			var zorderInfo = _data.ZorderInfo[zorder];

			if (currentState == CheckState.Out)return;

			if (currentState == CheckState.NearIn)
			{
			    for (int i = zorderInfo.Start; i < zorderInfo.End; i++)
			    {
			        var info = colliderFunc(_data.Data[i].Collider);
			        if (info.IsHit)
			        {
			            result.Add(_data.Data[i].Collider);
			        }
			    }
			}

			if (currentState == CheckState.AllIn)
			{
				for (int i = zorderInfo.Start; i < zorderInfo.End; i++)
				{
					result.Add(_data.Data[i].Collider);
				}
			}
            
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int nextZorder = zorder * 4 + 1 + i + j * 2;
					if (nextZorder >= _data.Setting.MaxZOrder) continue;
					if (_data.ZorderInfo[nextZorder].Count == 0) continue;

					float dx = -area.Size.x / 4 + i * area.Size.x / 2;
					float dy = -area.Size.y / 4 + j * area.Size.y / 2;
					var nextArea = new Rectangle(area.Position + new Vector2(dx, dy), area.Size / 2, area.Rotation);

					RecursiveCheck(areaFunc, colliderFunc, result, nextZorder, nextArea, currentState);
				}
			}
		}
	}
}