
using System;
using System.Collections.Generic;
using Stool.CSharp;
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
	partial class CircleCollisionSearcher
	{
		private CircleCollisionData _data;

		public enum CheckState
		{
			AllIn,
			NearIn,
			Out
		}

		public CircleCollisionSearcher(CircleCollisionData data)
		{
			_data = data;
		}

		public List<CircleCollisionInfo> Check(Rectangle rect)
		{
			var ret = new List<CircleCollisionInfo>();

			RecursiveCheck(
				(x, y) => CircleCollisionCheckFunctions.AreaCheck_Rectangle(rect, x, y),
				x => CircleCollisionCheckFunctions.CircleCheck_Rectangle(rect, x),
				ret, 0, _data.Setting.GetAreaRectangle(), CheckState.NearIn);

			return ret;
		}

	    public List<CircleCollisionInfo> Check(ICircleCollisionChecker checker)
	    {
	        var ret = new List<CircleCollisionInfo>();

            RecursiveCheck(
                checker.CircleCollision_AreaCheck, checker.CircleCollision_ColliderCheck,
                ret,0,_data.Setting.GetAreaRectangle(), CheckState.NearIn);

	        return ret;
	    }

        private void RecursiveCheck(
			Func<Rectangle, float, CheckState> areaFunc,
			Func<ICircleCollider, CircleCollisionInfo> colliderFunc,
			List<CircleCollisionInfo> result,
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

			if (currentState == CheckState.NearIn || currentState == CheckState.AllIn)
			{
			    for (int i = zorderInfo.Start; i < zorderInfo.End; i++)
			    {
			        var info = colliderFunc(_data.Data[i].Collider);
			        if (info.IsHit)
			        {
			            result.Add(info);
			        }
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