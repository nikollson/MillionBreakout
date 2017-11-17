using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] private GameObject _ui;

    void Update()
    {
        if (ButtleSystem.Instance.ParameterManager.HP <= 0)
        {
            _ui.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
