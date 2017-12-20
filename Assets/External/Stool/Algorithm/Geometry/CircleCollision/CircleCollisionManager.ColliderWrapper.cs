
using System.Collections;
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
	partial class CircleCollisionManager
	{
		class ColliderWrapper : IComparer
		{
			private ICircleCollider _collider;

			public bool Removed { get; private set; }
			public int ZOrder { get; private set; }

			public ColliderWrapper(ICircleCollider collider)
			{
				_collider = collider;
			}

			public void SetRemove()
			{
				Removed = true;
			}

		    public void UpdateZOrder(CircleCollisionSetting setting)
		    {
		        ZOrder = ZOrderCalculater.GetZOrder(_collider, setting);
		    }

		    public int Compare(object argx, object argy)
		    {
		        var x = (ColliderWrapper) argx;
		        var y = (ColliderWrapper) argy;

		        if (y == null || y.Removed) return -1;
		        if (x == null || x.Removed) return 1;

		        return x.ZOrder - y.ZOrder;
		    }
		}
	}
}
