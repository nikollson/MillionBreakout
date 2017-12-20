
using System;
using Stool.CSharp;
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
	class CircleCollisionChecker
	{
		private ValuePair<int, int>[] _zorderIndices;

		private CircleCollisionColliders _colliders;

		public CircleCollisionChecker(CircleCollisionColliders colliders)
		{
			_colliders = colliders;
		}

		public void UpdateSetting(CircleCollisionSetting setting)
		{
			_zorderIndices = new ValuePair<int, int>[setting.MaxZOrder];
			for (int i = 0; i < _zorderIndices.Length; i++) _zorderIndices[i] = new ValuePair<int, int>(0, 0);
		}
		
		public void Reflesh()
		{
			foreach (var zorderIndex in _zorderIndices)
			{
				zorderIndex.Item1 = -1;
				zorderIndex.Item2 = -1;
			}

			int cnt = 0;
			foreach (var collider in _colliders)
			{
				int zorder = collider.ZOrder;

				if (_zorderIndices[zorder].Item1 == -1)
				{
					_zorderIndices[zorder].Item1 = cnt;
				}
				_zorderIndices[zorder].Item2 = Math.Max(_zorderIndices[zorder].Item2, cnt + 1);

				cnt++;
			}
		}
	}
}