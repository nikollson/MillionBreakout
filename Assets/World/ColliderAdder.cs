using UnityEngine;
using System.Collections;
using Stool.MilllionBullets;

public class ColliderAdder : MonoBehaviour {

    void Start()
    {
        var blocksCollider = GetComponent<MillionBulletsBlocksCollider>();
        var boxCollider = GetComponent<MillionBulletsBoxCollider>();

        if (blocksCollider != null)
        {
            MillionBulletsManager.Instance.AddBlocksCollider(blocksCollider);
            return;
        }
        if (boxCollider != null)
        {
            MillionBulletsManager.Instance.AddBoxCollider(boxCollider);
        }
    }
}
