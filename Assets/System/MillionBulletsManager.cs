using System.Collections.Generic;
using UnityEngine;
using Stool.MilllionBullets;
using Stool.MilllionBullets.Collision2D;
using Stool.MilllionBullets.Sample;

class MillionBulletsManager : MonoBehaviour
{
    [SerializeField] private ComputeShader _collisionComputeShader;
    [SerializeField] private ComputeShader _emptyIndexComputeShader;
    public ColorBallFunctions ColorBallFunctions;

    public static MillionBulletsManager Instance { get; private set; }

    private List<MillionBulletsBlocksCollider> _blocksColliders;
    private List<MillionBulletsBoxCollider> _boxColliders;

    private BulletsCollision _bulletsCollision;
    public BulletsBuffer<ColorBallOption> ColorBallBuffer { get; private set; }

    void Awake()
    {
        Instance = this;
        _blocksColliders = new List<MillionBulletsBlocksCollider>();
        _boxColliders = new List<MillionBulletsBoxCollider>();
        _bulletsCollision = new BulletsCollision(_collisionComputeShader);
        ColorBallBuffer = new BulletsBuffer<ColorBallOption>(ColorBallFunctions, _emptyIndexComputeShader);

        _bulletsCollision.Adder.AddBuffer(ColorBallBuffer);
        AddDeadLineBalls();
    }

    public void AddBlocksCollider(MillionBulletsBlocksCollider collider)
    {
        _blocksColliders.Add(collider);
    }

    public void AddBoxCollider(MillionBulletsBoxCollider collider)
    {
        _boxColliders.Add(collider);
    }

    void Update()
    {
        ColorBallBuffer.Update(Time.deltaTime);
        foreach (var collider in _blocksColliders)
        {
            if (collider == null||collider.gameObject==null) continue;
            _bulletsCollision.Searcher.CheckBlocksCollision(collider, collider.mode);
        }
        foreach (var collider in _boxColliders)
        {
            if (collider == null||collider.gameObject==null) continue;
            _bulletsCollision.Searcher.CheckBoxCollision(collider, collider.mode);
        }
    }

    void OnRenderObject()
    {
        ColorBallBuffer.Render();
    }

    void OnDestroy()
    {
        ColorBallBuffer.Release();
        _bulletsCollision.Release();
    }

    void AddDeadLineBalls()
    {
        int N = 30;
        var states = new BulletState[N];
        var options = new ColorBallOption[N];
        var width = ButtleSystem.Instance.Stage.Width * 0.98f;
        var center = ButtleSystem.Instance.Stage.Center;

        for (int i = 0; i < N; i++)
        {
            var x = center.x + i * width / N - width / 2;
            var y = ButtleSystem.Instance.Stage.EnemyDeadLineY - Random.Range(0,0.4f);
            states[i] = new BulletState(
                new Vector2(x,y), Vector3.zero, ColorBallFunctions.GetRadius());
            options[i] = new ColorBallOption(Color.red);
        }
        ColorBallBuffer.Adder.AddBullets(states,options);
    }
}
