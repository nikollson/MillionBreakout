using UnityEngine;

namespace StoolKit.MilllionBullets
{
    [RequireComponent(typeof(MillionBulletsBlocksCollider))]
    public class MillionBulletsBlocksRenderer: MonoBehaviour
    {
        [SerializeField] private Texture2D _texture;
        [SerializeField] private Shader _shader;

        private Material _material;
        private MillionBulletsBlocksCollider _blocksCollider;

        public void Awake()
        {
            _blocksCollider = GetComponent<MillionBulletsBlocksCollider>();
            _material = new Material(_shader);
        }

        public void OnRenderObject()
        {
            var info = _blocksCollider.GetBlocksInfo();
            var buffer = _blocksCollider.BlockElementsBuffer;

            _material.SetTexture("_MainTex", _texture);
            _material.SetBuffer("BlockElements", buffer);
            _material.SetVector("BoxCenter", info.Box.Center);
            _material.SetFloat("BoxAngle", info.Box.Angle);
            _material.SetFloat("BoxWidth", info.Box.Width);
            _material.SetFloat("BoxHeight", info.Box.Height);
            _material.SetFloat("DivideX", 1.0f / info.ArrayWidth);
            _material.SetFloat("DivideY", 1.0f / info.ArrayHeight);
            _material.SetPass(0);

            Graphics.DrawProcedural(MeshTopology.Points, buffer.count);
        }
    }
}
