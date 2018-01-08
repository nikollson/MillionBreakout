using System;
using UnityEngine;

namespace MillionBreakout
{
    public class ButtleThouchScreen : MonoBehaviour
    {
        public bool IsTouching { get; private set; }
        public Vector2 TouchPosition { get; private set; }

        public Action<Vector2> OnStartTouch { get; set; }
        public Action<Vector2> OnUpdateTouch { get; set; }
        public Action<Vector2> OnEndTouch { get; set; }

        public void StartTouch()
        {
            if (IsTouching) return;

            IsTouching = true;
            TouchPosition = GetTouchPosition();

            if (OnStartTouch != null)
            {
                OnStartTouch(Vector2.down);
            }
        }

        public void UpdateTouch()
        {
            TouchPosition = GetTouchPosition();

            if (OnUpdateTouch != null)
            {
                OnUpdateTouch(Vector2.down);
            }
        }

        public void EndTouch()
        {
            if (IsTouching == false) return;

            IsTouching = false;
            TouchPosition = GetTouchPosition();

            if (OnEndTouch != null)
            {
                OnEndTouch(Vector2.down);
            }
        }

        public void Update()
        {
            if (IsTouching)
            {
                UpdateTouch();
            }
        }

        private Vector2 GetTouchPosition()
        {
            var position = Input.mousePosition;

            if (Input.touchCount != 0)
            {
                position = Input.touches[0].position;
            }

            return position;
        }
    }
}