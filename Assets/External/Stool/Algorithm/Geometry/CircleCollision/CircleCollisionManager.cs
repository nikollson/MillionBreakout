
using System;
using System.Collections.Generic;
using Stool.CSharp;

namespace Stool.Algorithm.Geometry
{
    public class CircleCollisionManager
    {
        public CircleCollisionData Data { get; private set; }
        public CircleCollisionSearcher Searcher { get; private set; }

        public CircleCollisionManager(CircleCollisionSetting setting)
        {
            Data = new CircleCollisionData(setting);
            Searcher = new CircleCollisionSearcher(Data);
            UpdateSetting(setting);
        }

        public void UpdateSetting(CircleCollisionSetting setting)
        {
            Data.UpdateSetting(setting);
        }

        public void UpdateColliderInfo()
        {
            Data.RefreshColliderInfo();
        }

        public void AddCollider(ICircleCollider collider)
        {
            Data.Add(collider);
        }

        public void RemoveCollider(ICircleCollider collider)
        {
            Data.Remove(collider);
        }
    }
}
