using UnityEngine;
using System.Collections;

class ButtleSystem : MonoBehaviour
{
    public static ButtleSystem Instance;

    public Camera Camera;
    public PlayerBar PlayerBar;
    public UnitMaker UnitMaker;
    public ParameterManager ParameterManager;

    void Awake()
    {
        Instance = this;
    }
}
