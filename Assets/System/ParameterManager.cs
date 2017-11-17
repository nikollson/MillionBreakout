using System.Runtime.InteropServices;
using UnityEngine;

public class ParameterManager : MonoBehaviour
{

    public float HPMax;
    public float HP { get; private set; }

    public int InitialCoin;
    public int Coin { get; private set; }

    public int LevelNum { get; private set; }
    public float LevelTime { get; private set; }

    [SerializeField] private int _initialLevel;

    void Awake()
    {
        HP = HPMax;
        Coin = InitialCoin;
        SetLevel(_initialLevel);
    }

    void Update()
    {
        LevelTime += DeltaTime;
    }

    public void SetLevel(int level)
    {
        LevelNum = level;
        LevelTime = 0;
    }

    public void CoinAdd(int val)
    {
        Coin += val;
    }

    public void HPAdd(int val)
    {
        HP += val;
        HP = Mathf.Min(HPMax, Mathf.Max(0, HP));
    }

    public float DeltaTime
    {
        get { return Time.deltaTime; }
    }
}
