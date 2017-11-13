
using UnityEditor;
using UnityEngine;

namespace Stool.MilllionBullets.Sample
{
    class FunctionsManagerSample : MonoBehaviour
    {
        [SerializeField] private ComputeShader _emptyIndexComputeShader;
        [SerializeField] private ColorBallFunctions _colorBallFunctions;

        public BulletsBuffer<ColorBallBulletOption> ColorBallBuffer { get; private set; }

        void Awake()
        {
            ColorBallBuffer = new BulletsBuffer<ColorBallBulletOption>(_colorBallFunctions,_emptyIndexComputeShader);

            int len = _colorBallFunctions.GetLength()-100;
            var states = new State[len];
            var options = new ColorBallBulletOption[len];
            for (int i = 0; i < len; i++)
            {
                states[i] =
                    new State(
                        new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f),
                            Random.Range(-10.0f, 10.0f)),1);
                options[i] =
                    new ColorBallBulletOption(
                        new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 0.5f,
                        new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));

                float sum = options[i].Color.x + options[i].Color.y + options[i].Color.x;
                if (sum < 2)
                {
                    states[i].Enable = 0;
                }
            }
            ColorBallBuffer.AddBullets(states, options);
        }

        private int cnt = 0;
        void Update()
        {
            ColorBallBuffer.Update();
        }

        void OnRenderObject()
        {
            ColorBallBuffer.Render();
        }

        void OnDisable()
        {
            ColorBallBuffer.Release();
        }
    }
}
