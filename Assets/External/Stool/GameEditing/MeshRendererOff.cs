
using UnityEngine;

namespace Stool.GameEditing
{
    class MeshRendererOff : MonoBehaviour
    {
        public void Start()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
