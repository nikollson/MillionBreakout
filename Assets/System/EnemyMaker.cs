using UnityEngine;
using System.Collections;

class EnemyMaker : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelEnemyObject;

    void Awake()
    {
        for (int i = 0; i < _levelEnemyObject.Length; i++)
        {
            _levelEnemyObject[i].SetActive(false);
        }
    }

    void Update()
    {
        int level = ButtleSystem.Instance.ParameterManager.LevelNum;
        if (_levelEnemyObject[level].activeSelf == false)
        {
            _levelEnemyObject[level].SetActive(true);
        }

        if (_levelEnemyObject[level].gameObject.transform.childCount == 0)
        {
            if (level < _levelEnemyObject.Length - 1)
            {
                ButtleSystem.Instance.ParameterManager.SetLevel(level + 1);
            }
        }
    }
}
