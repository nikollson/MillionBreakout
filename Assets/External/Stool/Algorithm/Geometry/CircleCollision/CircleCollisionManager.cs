
using System;
using System.Collections.Generic;
using Stool.CSharp;

namespace Stool.Algorithm.Geometry
{
    partial class CircleCollisionManager
    {
        private List<ColliderWrapper> _colliders;
        private Dictionary<ICircleCollider, ColliderWrapper> _wrapperDictionary;
        private ValuePair<int, int>[] _zorderIndices;

        private CircleCollisionSetting _setting;

        public CircleCollisionManager(CircleCollisionSetting setting)
        {
            _colliders = new List<ColliderWrapper>();
            _wrapperDictionary = new Dictionary<ICircleCollider, ColliderWrapper>();

            UpdateSetting(setting);
        }

        public void UpdateSetting(CircleCollisionSetting setting)
        {
            _setting = setting;
            _zorderIndices = new ValuePair<int, int>[_setting.MaxZOrder];

            for(int i=0;i<_zorderIndices.Length;i++)_zorderIndices[i] = new ValuePair<int, int>(0,0);
        }

        public void AddCollider(ICircleCollider collider)
        {
            var wrapper = new ColliderWrapper(collider);

            _colliders.Add(wrapper);
            _wrapperDictionary.Add(collider, wrapper);
        }

        public void RemoveCollider(ICircleCollider collider)
        {
            var wrapper = _wrapperDictionary[collider];

            wrapper.SetRemove();
            _wrapperDictionary.Remove(collider);
        }
        
        public void UpdateColliderInfo()
        {
            _colliders.ForEach(x => x.UpdateZOrder(_setting));
            
            _colliders.Sort();

            foreach (var zorderIndex in _zorderIndices)
            {
                zorderIndex.Item1 = -1;
                zorderIndex.Item2 = -1;
            }

            for (int i = _colliders.Count - 1; i >= 0; i--)
            {
                var collider = _colliders[i];

                if (collider == null || _colliders[i].Removed)
                {
                    _colliders.RemoveAt(i);
                    continue;
                }

                int zorder = collider.ZOrder;
                _zorderIndices[zorder].Item1 = i;
                _zorderIndices[zorder].Item2 = Math.Max(_zorderIndices[zorder].Item2, i + 1);
            }
        }

        public string Dump()
        {
            string ret = "";

            for (int i = 0; i < _colliders.Count; i++)
            {
                var element = _colliders[i];
                var pos = element.Collider.GetColliderCenter();
                ret += "" + element.ZOrder + " : " + pos + "\n";
            }

            for (int i = 0; i < _zorderIndices.Length; i++)
            {
                var element = _zorderIndices[i];
                if(element.Item1==-1)continue;
                
                ret += "(" + i + ":" + element.Item1 + ":" + element.Item2 + ")  ";
            }

            return ret;
        }
    }
}
