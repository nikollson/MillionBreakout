
using UnityEngine;

namespace Stool.MilllionBullets.Sample
{
    struct ColorBallBulletOption
    {
        public Vector3 Accel;
        public Vector4 Color;

        public ColorBallBulletOption(Vector3 accel, Vector4 color)
        {
            Accel = accel;
            Color = color;
        }
    }

    class ColorBallFunctions : BufferFunctionsBase<ColorBallBulletOption>
    {
        [SerializeField] private int _length;
        [SerializeField] private Shader _surfaceShader;
        [SerializeField] private ComputeShader _computeShader;
        [SerializeField] private Texture2D _texture;

        private Material _material;

        public void Awake()
        {
            _material = new Material(_surfaceShader);
        }

        public override void AddOptions(ComputeBuffer optionsBuffer, int[] indices, ColorBallBulletOption[] options)
        {
            var array = new ColorBallBulletOption[optionsBuffer.count];
            optionsBuffer.GetData(array);
            for (int i = 0; i < indices.Length; i++)
            {
                array[indices[i]] = options[indices[i]];
            }
            optionsBuffer.SetData(array);
        }

        public override void RenderBullets(ComputeBuffer statesBuffer, ComputeBuffer optionsBuffer)
        {
            _material.SetTexture("_MainTex", _texture);
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
            _computeShader.Dispatch(0, statesBuffer.count / 8 + 1, 1, 1);
        }

        public override int GetLength()
        {
            return _length;
        }
    }
}
