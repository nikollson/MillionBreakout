using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MillionBreakout
{

    public class AutoSceneLoader : MonoBehaviour
    {
        public string SceneName;

        public void Awake()
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
    }
}