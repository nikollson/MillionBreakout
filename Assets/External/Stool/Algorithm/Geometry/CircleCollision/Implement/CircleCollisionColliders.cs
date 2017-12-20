
using System;
using System.Collections;
using System.Collections.Generic;

namespace Stool.Algorithm.Geometry
{
    partial class CircleCollisionColliders : IEnumerable<CircleCollisionColliders.ColliderWrapper>
    {
        private List<ColliderWrapper> _data;
        private Dictionary<ICircleCollider, ColliderWrapper> _wrapperDictionary;

        public CircleCollisionColliders()
        {
            _data = new List<ColliderWrapper>();
            _wrapperDictionary = new Dictionary<ICircleCollider, ColliderWrapper>();
        }

        public void Add(ICircleCollider collider)
        {
            var wrapper = new ColliderWrapper(collider);

            _data.Add(wrapper);
            _wrapperDictionary.Add(collider, wrapper);
        }

        public void Remove(ICircleCollider collider)
        {
            var wrapper = _wrapperDictionary[collider];

            wrapper.SetRemove();
            _wrapperDictionary.Remove(collider);
        }

        public void SortColliders(CircleCollisionSetting setting)
        {
            foreach (var wrapper in _data)
            {
                int zorder = CircleCollisionManager.ZOrderCalculater.GetZOrder(wrapper.Collider, setting);
                wrapper.UpdateZOrder(zorder);
            }

            _data.Sort();

        }

        public void RemoveUnusedColliders()
        {
            for (int i = _data.Count - 1; i >= 0; i--)
            {
                var collider = _data[i];

                if (collider == null || collider.Removed)
                {
                    _data.RemoveAt(i);
                    continue;
                }
            }
        }
        public IEnumerator<ColliderWrapper> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public class ColliderWrapper : IComparable<ColliderWrapper>
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

            public void UpdateZOrder(int zorder)
            {
                ZOrder = zorder;
            }

            public int CompareTo(ColliderWrapper x)
            {
                if (x.Removed)
                    return -1;
                if (Removed)
                    return 1;

                return ZOrder - x.ZOrder;
            }
        }

    }
}
