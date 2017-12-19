
using System;
using UniRx.Operators;
using UnityEngine;

namespace StoolKit.ThousandBullets
{
    class ThousandBulletPrefab : MonoBehaviour
    {
        public Material DefaultMaterial;
        public Texture2D DefaultTexture;

        public MeshRenderer MeshRenderer
        {
            get { return GetComponent<MeshRenderer>(); }
        }

        private ThousandBulletBehaviour _behaviour;
        
        public void SerBehaviour(ThousandBulletBehaviour behaviour)
        {
            _behaviour = behaviour;

            Initialize();

            _behaviour.SetPrefab(this);
            _behaviour.Start();

            MeshRenderer.enabled = true;
        }

        private void Initialize()
        {
            var material = _behaviour.GetInitalMaterial();
            if (material == null) material = DefaultMaterial;
            if (material.shader != MeshRenderer.material.shader)
            {
                MeshRenderer.material = material;
            }

            var texture = _behaviour.GetInitialTexture();
            if (texture == null) texture = DefaultTexture;
            if (texture != MeshRenderer.material.mainTexture)
            {
                MeshRenderer.material.mainTexture = texture;
            }
        }

        public void ClearBehaviour()
        {
            _behaviour.End();
            _behaviour = null;

            MeshRenderer.enabled = false;
        }

        public bool IsEnable()
        {
            if (_behaviour == null) return false;
            return _behaviour.IsEnable();
        }

        public void Update()
        {
            if(_behaviour == null)return;
            _behaviour.Update();
        }
    }
}
