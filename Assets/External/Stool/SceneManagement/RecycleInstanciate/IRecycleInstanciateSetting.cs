
using UnityEngine;

namespace Stool.SceneManagement
{
    interface IRecycleInstanciateSetting
    {
        GameObject GetDefaltPrefab();
        void SetData(GameObject gameObject);
        void ClearData(GameObject gameObject);
        bool IsEnable();
    }
}
