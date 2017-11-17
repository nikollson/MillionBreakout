using UnityEngine;
using System.Collections;
using Stool.MilllionBullets;

[RequireComponent(typeof(MillionBulletsBoxCollider))]
public class BumperUnit : MonoBehaviour
{
    [SerializeField] private int _HP;
    [SerializeField] private TextMesh _restText;
    private MillionBulletsBoxCollider _boxCollider;
    public int Rest { get { return Mathf.Max(0,_HP - _boxCollider.HitCount); } }

    void Awake()
    {
        _boxCollider = GetComponent<MillionBulletsBoxCollider>();
    }

    void LateUpdate()
    {
        _restText.text = "" + Rest;
        if (Rest <= 0)
        {
            Destroy(gameObject);
        }
    }
}
