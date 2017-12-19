
using UnityEngine;

namespace Stool.SceneManagement
{
    interface IRecycleInstanceController
    {
        void SetData(GameObject gameObject);
        void ClearData(GameObject gameObject);
    }
}
