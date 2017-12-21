
using System.Linq.Expressions;
using UnityEngine;

namespace Tkool.MilllionBullets.Sample
{
    class BufferSampleManager : MonoBehaviour
    {
        [SerializeField] private ComputeShader _emptyIndexComputeShader;
        [SerializeField] private ColorBallFunctions _colorBallFunctions;
        
        public BulletsBuffer<ColorBallOption> ColorBallBuffer { get; private set; }

        void Awake()
        {
            ColorBallBuffer = new BulletsBuffer<ColorBallOption>(_colorBallFunctions,_emptyIndexComputeShader);
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
                            new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f),0),
                            new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0), 
                            0.1f);
                    options[i] =
                        new ColorBallOption(
                            new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
                }
                ColorBallBuffer.Adder.AddBullets(states, options);
            }
            ColorBallBuffer.Update(Time.deltaTime);
        }

        void OnRenderObject()
        {
            ColorBallBuffer.Render();
        }

        void OnDestroy()
        {
            ColorBallBuffer.Release();
        }
    }
}
