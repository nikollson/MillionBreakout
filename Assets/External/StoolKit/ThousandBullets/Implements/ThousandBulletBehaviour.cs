
using UnityEngine;

namespace StoolKit.ThousandBullets
{
    abstract class ThousandBulletBehaviour
    {
        public Transform Transform { get { return _bulletPrefab.transform; } }
        public MeshRenderer MeshRenderer { get { return _bulletPrefab.MeshRenderer; } }

        private ThousandBulletPrefab _bulletPrefab;
        private bool _isEnable;

        public void SetPrefab(ThousandBulletPrefab bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
            _isEnable = true;
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

        public void DestroyThisBullet()
        {
            _isEnable = false;
        }

        public bool IsEnable()
        {
            return _isEnable;
        }
    }
}
