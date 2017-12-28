
using System;
using System.Collections;
using System.Collections.Generic;
using Stool.CSharp;
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    public partial class CircleCollisionData
    {
        public List<ColliderWrapper> Data { get; private set; }
        public IndexInfo[] ZorderInfo { get; private set; }
        public CircleCollisionSetting Setting { get; private set; }

        private Dictionary<ICircleCollider, ColliderWrapper> _wrapperDictionary;


        public CircleCollisionData(CircleCollisionSetting setting)
        {
            Data = new List<ColliderWrapper>();
            _wrapperDictionary = new Dictionary<ICircleCollider, ColliderWrapper>();

            UpdateSetting(setting);
        }

        public void Add(ICircleCollider collider)
        {
            var wrapper = new ColliderWrapper(collider);

            Data.Add(wrapper);
            _wrapperDictionary.Add(collider, wrapper);
        }

        public void Remove(ICircleCollider collider)
        {
            var wrapper = _wrapperDictionary[collider];

            wrapper.SetRemove();
            _wrapperDictionary.Remove(collider);
        }

        public void RefreshColliderInfo()
        {
            foreach (var wrapper in Data)
            {
                int zorder = ZOrderCalculater.GetZOrder(wrapper.Collider, Setting);
                wrapper.UpdateZOrder(zorder);
            }

            Data.Sort();

            for (int i = Data.Count - 1; i >= 0; i--)
            {
                var collider = Data[i];

                if (collider == null || collider.Removed)
                {
                    Data.RemoveAt(i);
                }
            }

            foreach (var zorderIndex in ZorderInfo)
            {
                zorderIndex.Start = -1;
                zorderIndex.End = -1;
                zorderIndex.Count = 0;
            }

            for (int i = Data.Count - 1; i >= 0; i--)
            {
                int zorder = Data[i].ZOrder;
                ZorderInfo[zorder].Start = i;
                ZorderInfo[zorder].End = Math.Max(ZorderInfo[zorder].End, i + 1);
                ZorderInfo[zorder].Count++;
            }

            for (int i = ZorderInfo.Length - 1; i >= 1; i--)
            {
                ZorderInfo[(i - 1) / 4].Count += ZorderInfo[i].Count;
            }
        }

        public void UpdateSetting(CircleCollisionSetting setting)
        {
            Setting = setting;
            if (ZorderInfo == null || ZorderInfo.Length != setting.MaxZOrder)
            {
                ZorderInfo = new IndexInfo[setting.MaxZOrder];
                for (int i = 0; i < ZorderInfo.Length; i++)
                {
                    ZorderInfo[i] = new IndexInfo();
                }
            }
            
        }

        public string DumpString()
        {
            string ret = "";
            ret += "ZOrder\n";
            for (int i = 0; i < ZorderInfo.Length; i++)
            {
                if (ZorderInfo[i].Start == -1) continue;
                ret += "(" + i + " : " + ZorderInfo[i].Start + " " + ZorderInfo[i].End + " " + ZorderInfo[i].Count +
                       ")  ";
            }
            return ret;
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

        public class IndexInfo
        {
            public int Start { get; set; }
            public int End { get; set; }
            public int Count { get; set; }
        }
    }
}
