
using UnityEngine;

namespace Stool.MilllionBullets.Sample
{
    struct ColorBallOption
    {
        public Vector3 Accel;
        public Vector4 Color;

        public ColorBallOption(Vector3 accel, Vector4 color)
        {
            Accel = accel;
            Color = color;
        }
    }

    class ColorBallFunctions : BufferFunctionsBase<ColorBallOption>
    {
        [SerializeField] private int _length;
        [SerializeField] private Shader _surfaceShader;
        [SerializeField] private ComputeShader _computeShader;
        [SerializeField] private Texture2D _texture;

        private Material _material;

        private readonly int ThreadNum = 8;

        public void Awake()
        {
            _material = new Material(_surfaceShader);
            _material.SetTexture("_MainTex", _texture);
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
            _material.SetPass(0);
            Graphics.DrawProcedural(MeshTopology.Points, statesBuffer.count);
        }

        public override void UpdateBullets(ComputeBuffer statesBuffer, ComputeBuffer optionsBuffer)
        {
            _computeShader.SetBuffer(0, "States", statesBuffer);
            _computeShader.SetBuffer(0, "Options", optionsBuffer);
            _computeShader.SetFloat("DeltaTime", Time.deltaTime);
            _computeShader.SetFloat("ColorDecSpeed", 0.2f);
            _computeShader.Dispatch(0, statesBuffer.count / ThreadNum + 1, 1, 1);
        }

        public override int GetLength()
        {
            return _length;
        }
    }
}
