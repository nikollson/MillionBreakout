using UnityEngine;
using System.Collections;

class ButtleSystem : MonoBehaviour
{
    public static ButtleSystem Instance;

    public Camera Camera;
    public PlayerBar PlayerBar;

    void Awake()
    {
        Instance = this;
    }
}
