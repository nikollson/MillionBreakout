
using UnityEngine;

namespace Stool.SceneManagement
{
    public interface IRecycleInstanceController
    {
        void SetData(GameObject gameObject);
        void ClearData(GameObject gameObject);
    }
}
