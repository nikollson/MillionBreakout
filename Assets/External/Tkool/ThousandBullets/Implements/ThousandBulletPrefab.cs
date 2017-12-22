
using System;
using UniRx.Operators;
using UnityEngine;

namespace Tkool.ThousandBullets
{
    class ThousandBulletPrefab : MonoBehaviour
    {
        public Material DefaultMaterial;
        public Texture2D DefaultTexture;

        private float previousRaidus = 0.5f;

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
            _behaviour.OnStartBullet();

            MeshRenderer.enabled = true;
        }

        private void Initialize()
        {
            var material = _behaviour.GetInitalBulletMaterial();
            if (material == null) material = DefaultMaterial;
            if (material.shader != MeshRenderer.material.shader)
            {
                MeshRenderer.material = material;
            }

            var texture = _behaviour.GetInitialBulletTexture();
            if (texture == null) texture = DefaultTexture;
            if (texture != MeshRenderer.material.mainTexture)
            {
                MeshRenderer.material.mainTexture = texture;
            }

            UpdateRadius();
        }

        private void UpdateRadius()
        {
            var radius = _behaviour.GetBulletRadius();
            var expend = radius / previousRaidus;

            if (expend != 1.0f)
            {
                transform.localScale = transform.localScale * expend;
            }

            previousRaidus = radius;
        }

        public void ClearBehaviour()
        {
            _behaviour.OnEndBullet();
            _behaviour = null;

            MeshRenderer.enabled = false;
        }

        public void Update()
        {
            if(_behaviour == null)return;
            _behaviour.OnUpdateBullet();

            UpdateRadius();
        }
    }
}
