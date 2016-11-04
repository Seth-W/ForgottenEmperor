namespace CFE
{

    using UnityEngine;
    using System.Collections;

    class InputManager : MonoBehaviour
    {
        public delegate void FrameInput(FrameInputData data);

        public static FrameInput FrameInputEvent;

        bool _isPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _isPaused = !_isPaused;
            FrameInputEvent(new FrameInputData(Input.mousePosition, _isPaused));
        }
    }
}