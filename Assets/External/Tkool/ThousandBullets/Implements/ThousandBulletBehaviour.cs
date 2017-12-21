
using UnityEngine;

namespace Tkool.ThousandBullets
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

        public virtual void OnStartBullet()
        {
            
        }

        public virtual void OnUpdateBullet()
        {
            
        }

        public virtual void OnEndBullet()
        {
            
        }

        public virtual float GetBulletRadius()
        {
            return 0.5f;
        }

        public virtual Material GetInitalBulletMaterial()
        {
            return null;
        }

        public virtual Texture2D GetInitialBulletTexture()
        {
            return null;
        }
    }
}
