using UnityEngine;

namespace Stool.GameEditing
{
    public class SpriteRendererOff : MonoBehaviour {

        void Awake()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
