
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace StoolKit.ThousandBullets.Sample
{
    class NormalBulletInstance : ThousandBulletBehaviour
    {
        private int _count = 0;
        private Setting _setting;

        public NormalBulletInstance(Setting setting)
        {
            _setting = setting;
        }

        public override Material GetInitalBulletMaterial()
        {
            return _setting.Material;
        }

        public override Texture2D GetInitialBulletTexture()
        {
            return _setting.Texture;
        }

        public override void OnUpdateBullet()
        {
            _count++;

            Transform.position += new Vector3(0, 0.1f, 0);

            if (_count > 120)
            {
                DestroyThisBullet();
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
