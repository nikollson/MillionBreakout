using UnityEngine;

namespace Stool.Analytics
{
    public class FPSViewer : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Text _textUI;


        int frameCount;
        float prevTime;
        private int prevFrame = 0;

        void Awake()
        {
            Application.targetFrameRate = 30;
        }


        void Start()
        {
            frameCount = 0;
            prevTime = 0.0f;
        }

        void Update()
        {
            float time = Time.realtimeSinceStartup - prevTime;

            if (time >= 0.5f)
            {
                _textUI.text = ((Time.frameCount-prevFrame) / time).ToString("F1");
                prevFrame = Time.frameCount;
                prevTime = Time.realtimeSinceStartup;
            }
        }
    }
}
