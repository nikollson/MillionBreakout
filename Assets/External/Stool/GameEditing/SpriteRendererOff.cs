using UnityEngine;

namespace Stool.GameEditing
{
    public class SpriteRendererOff : MonoBehaviour {

        void Start()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
