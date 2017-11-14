using UnityEngine;

namespace Stool.MilllionBullets.Sample
{
    class CollisionSampleManager : MonoBehaviour
    {
        [SerializeField] private ComputeShader _emptyIndexComputeShader;
        [SerializeField] private ColorBallFunctions _colorBallFunctions;

        public BulletsBuffer<ColorBallOption> ColorBallBuffer { get; private set; }

        void Awake()
        {
            ColorBallBuffer = new BulletsBuffer<ColorBallOption>(_colorBallFunctions, _emptyIndexComputeShader);

        }

        private int cnt = 0;
        void Update()
        {
            cnt += 1;
            if (cnt % 50 == 0)
            {
                int len = ColorBallBuffer.GetRestAddlessSize();
                var states = new BulletState[len];
                var options = new ColorBallOption[len];
                for (int i = 0; i < len; i++)
                {
                    states[i] =
                        new BulletState(
                            new Vector3(Random.Range(-3.0f, 3.0f),0, 0));
                    options[i] =
                        new ColorBallOption(
                            new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 3.0f), 0),
                            new Color(Random.Range(0.6f, 1.0f), Random.Range(0.4f, 0.8f), Random.Range(0.45f, 0.5f)));
                }
                ColorBallBuffer.Adder.AddBullets(states, options);
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
