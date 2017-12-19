
using System;
using UnityEngine;

namespace StoolKit.ThousandBullets
{
    abstract class ThousandBulletBehaviour
    {
        public Transform Transform { get { return _bulletPrefab.transform; } }
        public MeshRenderer MeshRenderer { get { return _bulletPrefab.MeshRenderer; } }

        private ThousandBulletPrefab _bulletPrefab;

        public void SetPrefab(ThousandBulletPrefab bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void End()
        {
            
        }

        public virtual Material GetInitalMaterial()
        {
            return null;
        }

        public virtual Texture2D GetInitialTexture()
        {
            return null;
        }

        public abstract bool IsEnable();
    }
}
