
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
        }

        private int cnt = 0;
        void Update()
        {
            cnt += 1;
            if (cnt % 20 == 0)
            {
                int len = ColorBallBuffer.GetRestAddlessSize();
                var states = new BulletState[len];
                var options = new ColorBallBulletOption[len];
                for (int i = 0; i < len; i++)
                {
                    states[i] =
                        new BulletState(
                            new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f),
                                Random.Range(-10.0f, 10.0f)));
                    options[i] =
                        new ColorBallBulletOption(
                            new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 0.5f,
                            new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
                }
                ColorBallBuffer.AddBullets(states, options);
            }
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
