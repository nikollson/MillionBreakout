using UnityEngine;
using System.Collections;

class PlayerBar : MonoBehaviour
{
    [SerializeField] private Transform _touchCenter;

    public void SetMovePosition_Touch(Vector3 position)
    {

        Vector3 next = position + (transform.position - _touchCenter.position);
        next.y = Mathf.Max(next.y, ButtleSystem.Instance.Stage.EnemyDeadLineY + 0.2f);
        transform.position = next;
    }
}
