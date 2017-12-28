
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Tkool.ThousandBullets.Sample
{
    public class NormalBulletInstance : ThousandBulletBehaviour
    {
        private int _count = 0;
        private Setting _setting;

        public bool IsDead { get; private set; }

        public NormalBulletInstance(Setting setting)
        {
            _setting = setting;
            IsDead = false;
        }

        public override float GetBulletRadius()
        {
            return 0.5f;
        }

        public override Material GetInitalBulletMaterial()
        {
            return _setting.Material;
        }

        public override Texture2D GetInitialBulletTexture()
        {
            return _setting.Texture;
        }

        public override void OnUpdateBullet(float deltaTime)
        {
            _count++;

            Transform.position += new Vector3(0, 0.1f, 0);

            if (_count > 120)
            {
                IsDead = true;
            }
        }

        [Serializable]
        public class Setting
        {
            public Material Material;
            public Texture2D Texture;
        }
    }
}
