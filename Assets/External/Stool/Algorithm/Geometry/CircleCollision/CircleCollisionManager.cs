
using System;
using System.Collections.Generic;
using Stool.CSharp;

namespace Stool.Algorithm.Geometry
{
    partial class CircleCollisionManager
    {
        private CircleCollisionColliders Colliders;
        public CircleCollisionChecker Checker;

        private CircleCollisionSetting _setting;

        public CircleCollisionManager(CircleCollisionSetting setting)
        {
            _setting = setting;

            Colliders = new CircleCollisionColliders();
            Checker = new CircleCollisionChecker(Colliders);
            UpdateSetting(setting);
        }

        public void UpdateSetting(CircleCollisionSetting setting)
        {
            Checker.UpdateSetting(setting);
        }

        public void RefreshColliderInfo()
        {
            Colliders.SortColliders(_setting);
            Checker.Reflesh();
        }

        public void AddCollider(ICircleCollider collider)
        {
            Colliders.Add(collider);
        }

        public void RemoveCollider(ICircleCollider collider)
        {
            Colliders.Remove(collider);
        }
    }
}
