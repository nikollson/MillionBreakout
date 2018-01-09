using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{

    public string ButtleSceneName = "ButtleScene";

    public void LoadButtleScene()
    {
        SceneManager.LoadScene(ButtleSceneName);
    }
}
