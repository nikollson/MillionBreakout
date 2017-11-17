using UnityEngine;

public class WindowsScreenSizeSetter : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(720, 1280, false, 60);
    }
#endif
}