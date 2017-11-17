using UnityEngine;

public class UnitMaker : MonoBehaviour
{
    public void MakeUnit(GameObject prefab, Vector3 position)
    {
        var obj = (GameObject)Instantiate(prefab, position, Quaternion.identity);
        obj.transform.parent = transform;
    }
}
