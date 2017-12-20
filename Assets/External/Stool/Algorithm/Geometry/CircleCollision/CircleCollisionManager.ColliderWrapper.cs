
using System;
using System.Collections;
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
	partial class CircleCollisionManager
	{
		class ColliderWrapper : IComparable<ColliderWrapper>
		{
			public ICircleCollider Collider { get; private set; }
			public bool Removed { get; private set; }
			public int ZOrder { get; private set; }

			public ColliderWrapper(ICircleCollider collider)
			{
				Collider = collider;
			}

			public void SetRemove()
			{
				Removed = true;
			}

		    public void UpdateZOrder(CircleCollisionSetting setting)
		    {
		        ZOrder = ZOrderCalculater.GetZOrder(Collider, setting);
		    }

		    public int CompareTo(ColliderWrapper x)
		    {
		        if (x.Removed) return -1;
		        if (Removed) return 1;

		        return ZOrder - x.ZOrder;
		    }
		}
	}
}
