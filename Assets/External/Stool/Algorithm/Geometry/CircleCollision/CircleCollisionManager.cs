
using System;
using System.Collections.Generic;
using Stool.CSharp;

namespace Stool.Algorithm.Geometry
{
    partial class CircleCollisionManager
    {
        public CircleCollisionData Data { get; private set; }
        public CircleCollisionChecker Checker { get; private set; }

        public CircleCollisionManager(CircleCollisionSetting setting)
        {
            Data = new CircleCollisionData(setting);
            Checker = new CircleCollisionChecker(Data);
            UpdateSetting(setting);
        }

        public void UpdateSetting(CircleCollisionSetting setting)
        {
            Data.UpdateSetting(setting);
        }

        public void RefreshColliderInfo()
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
