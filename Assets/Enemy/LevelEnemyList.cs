using System;
using UnityEngine;
using System.Collections;

[CreateAssetMenu]
class LevelEnemyList : ScriptableObject
{
    public EnemyData[] EnemyDatas;

    [Serializable]
    public class EnemyData
    {
        public float Time;
        public GameObject Prefab;
    }
}
