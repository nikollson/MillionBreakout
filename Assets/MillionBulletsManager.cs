using System.Collections.Generic;
using UnityEngine;
using Stool.MilllionBullets;
using Stool.MilllionBullets.Collision2D;
using Stool.MilllionBullets.Sample;

class MillionBulletsManager : MonoBehaviour
{
    [SerializeField] private ComputeShader _collisionComputeShader;
    [SerializeField] private ComputeShader _emptyIndexComputeShader;
    [SerializeField] private ColorBallFunctions _colorBallFunctions;

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
        ColorBallBuffer = new BulletsBuffer<ColorBallOption>(_colorBallFunctions, _emptyIndexComputeShader);

        int N = _colorBallFunctions.GetLength();
        var states = new BulletState[N];
        var options = new ColorBallOption[N];
        for (int i = 0; i < N; i++)
        {
            states[i] = new BulletState(
                this.transform.position, new Vector3(Random.Range(-1.0f,1.0f),Random.Range(0.5f,1.5f),0), _colorBallFunctions.GetRadius());
            options[i] = new ColorBallOption(Color.white);
        }

        ColorBallBuffer.Adder.AddBullets(states,options);
        _bulletsCollision.Adder.AddBuffer(ColorBallBuffer);
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
            _bulletsCollision.Searcher.CheckBlocksCollision(collider, collider.mode);
        }
        foreach (var collider in _boxColliders)
        {
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
    }
}
