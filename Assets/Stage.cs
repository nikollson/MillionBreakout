using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour
{
    public float EnemyDeadLineY { get { return _deadLineTransform.position.y; } }
    public Vector3 Center { get { return transform.position; } }

    [SerializeField] private Transform _deadLineTransform;

    public float Width;
    public float Height;
}
