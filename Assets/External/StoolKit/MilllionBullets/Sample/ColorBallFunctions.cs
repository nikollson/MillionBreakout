
using UnityEngine;

namespace StoolKit.MilllionBullets.Sample
{
    struct ColorBallOption
    {
        public Vector4 Color;

        public ColorBallOption(Vector4 color)
        {
            Color = color;
        }
    }

    class ColorBallFunctions : BufferFunctionsBase<ColorBallOption>
    {
        [SerializeField] private int _length = 100;
        [SerializeField] private Shader _surfaceShader;
        [SerializeField] private ComputeShader _computeShader;
        [SerializeField] private Texture2D _texture;
        [SerializeField] private float _radius = 0.1f;

        private Material _material;

        private readonly int ThreadNum = 32;

        public void Awake()
        {
            _material = new Material(_surfaceShader);
        }

        public override void AddOptions(ComputeBuffer optionsBuffer, int n, ComputeBuffer indicesBuffer, ComputeBuffer inputBuffer)
        {
            int kernel = _computeShader.FindKernel("AddOptions");
            _computeShader.SetInt("N", n);
            _computeShader.SetBuffer(kernel, "Options", optionsBuffer);
            _computeShader.SetBuffer(kernel, "OptionsInput", inputBuffer);
            _computeShader.SetBuffer(kernel, "Indices", indicesBuffer);
            _computeShader.Dispatch(kernel, (n - 1) / ThreadNum + 1, 1, 1);
        }

        public override void RenderBullets(ComputeBuffer statesBuffer, ComputeBuffer optionsBuffer)
        {
            _material.SetBuffer("Options", optionsBuffer);
            _material.SetBuffer("States", statesBuffer);
            _material.SetTexture("_MainTex", _texture);
            _material.SetPass(0);
            Graphics.DrawProcedural(MeshTopology.Points, statesBuffer.count);
        }

        public override void UpdateBullets(ComputeBuffer statesBuffer, ComputeBuffer optionsBuffer)
        {
            int kernel = _computeShader.FindKernel("OnUpdateBullet");
            _computeShader.SetFloat("DeltaTime", Time.deltaTime);
            _computeShader.SetBuffer(kernel, "States", statesBuffer);
            _computeShader.SetBuffer(kernel, "Options", optionsBuffer);
            _computeShader.Dispatch(kernel, (statesBuffer.count-1) / ThreadNum + 1, 1, 1);
        }

        public override int GetLength()
        {
            return _length;
        }

        public float GetRadius()
        {
            return _radius;
        }
    }
}
