using Stool.MilllionBullets.Collision2D;
using UnityEngine;

namespace Stool.MilllionBullets.Sample
{
    class CollisionSampleManager : MonoBehaviour
    {
        [SerializeField] private ComputeShader _emptyIndexComputeShader;
        [SerializeField] private ComputeShader _collisionComputeShader;
        [SerializeField] private ColorBallFunctions _colorBallFunctions;
        [SerializeField] private ColorBallFunctions _colorBallFunctions2;
        [SerializeField] private MillionBulletsBoxCollider[] _boxColliders;

        public BulletsBuffer<ColorBallOption> ColorBallBuffer { get; private set; }
        public BulletsBuffer<ColorBallOption> ColorBallBuffer2 { get; private set; }
        public BulletsCollision BulletsCollision { get; private set; }

        void Awake()
        {
            ColorBallBuffer = new BulletsBuffer<ColorBallOption>(_colorBallFunctions, _emptyIndexComputeShader);
            ColorBallBuffer2 = new BulletsBuffer<ColorBallOption>(_colorBallFunctions2, _emptyIndexComputeShader);
            BulletsCollision = new BulletsCollision(_collisionComputeShader);
            BulletsCollision.Adder.AddBuffer(ColorBallBuffer);
            BulletsCollision.Adder.AddBuffer(ColorBallBuffer2);
        }

        void Update()
        {
            UpdateBuffer();
            foreach (var boxCollider in _boxColliders)
            {
                BulletsCollision.Searcher.CheckBoxCollision(boxCollider);
            }
        }

        void OnRenderObject()
        {
            ColorBallBuffer.Render();
            ColorBallBuffer2.Render();
        }

        void OnDisable()
        {
            ColorBallBuffer.Release();
            ColorBallBuffer2.Release();
            BulletsCollision.Release();
        }


        private int cnt = 0;
        void UpdateBuffer()
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
                            new Vector3(Random.Range(-3.0f, 3.0f), 0, 0),
                            new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 3.0f), 0),
                            _colorBallFunctions.GetRadius());
                    options[i] =
                        new ColorBallOption(
                            new Color(Random.Range(0.6f, 1.0f), Random.Range(0.4f, 0.8f), Random.Range(0.45f, 0.5f)));
                }
                ColorBallBuffer.Adder.AddBullets(states, options);
            }
            ColorBallBuffer.Update(Time.deltaTime);
            
            if (cnt % 60 == 0)
            {
                int len = ColorBallBuffer2.GetRestAddlessSize();
                var states = new BulletState[len];
                var options = new ColorBallOption[len];
                for (int i = 0; i < len; i++)
                {
                    states[i] =
                        new BulletState(
                            new Vector3(Random.Range(-3.0f, 3.0f), 0, 0),
                            new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 3.0f), 0),
                            _colorBallFunctions2.GetRadius());
                    options[i] =
                        new ColorBallOption(
                            new Color(Random.Range(0.6f, 1.0f), Random.Range(0.4f, 0.8f), Random.Range(0.45f, 0.5f)));
                }
                ColorBallBuffer2.Adder.AddBullets(states, options);
            }
            ColorBallBuffer2.Update(Time.deltaTime);
        }
    }
}
