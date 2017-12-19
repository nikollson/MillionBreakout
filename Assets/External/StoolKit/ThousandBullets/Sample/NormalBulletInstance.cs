
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace StoolKit.ThousandBullets.Sample
{
    class NormalBulletInstance : ThousandBulletBehaviour
    {
        private int _count = 0;
        private bool _isEnable = true;
        private Setting _setting;

        public NormalBulletInstance(Setting setting)
        {
            _setting = setting;
        }

        public override Material GetInitalMaterial()
        {
            return _setting.Material;
        }

        public override Texture2D GetInitialTexture()
        {
            return _setting.Texture;
        }

        public override void Update()
        {
            _count++;

            Transform.position += new Vector3(0, 0.1f, 0);

            if (_count > 120)
            {
                _isEnable = false;
            }
        }

        public override bool IsEnable()
        {
            return _isEnable;
        }

        [Serializable]
        public class Setting
        {
            public Material Material;
            public Texture2D Texture;
        }
    }
}
