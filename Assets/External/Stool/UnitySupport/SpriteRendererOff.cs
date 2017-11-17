using UnityEngine;

namespace Stool.UnitySupport
{
    public class SpriteRendererOff : MonoBehaviour {

        void Awake()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
