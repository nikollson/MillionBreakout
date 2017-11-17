using UnityEngine;
using System.Collections;

public class NormalBlocks : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;

    void Update()
    {
        this.transform.position += _speed * ButtleSystem.Instance.ParameterManager.DeltaTime;
    }   
}
