namespace CFE
{

    using UnityEngine;
    using System.Collections;

    class InputManager : MonoBehaviour
    {
        public delegate void FrameInput(FrameInputData data);

        public static FrameInput FrameInputEvent;

        static bool _isPaused = false;

        public static bool isPaused { get { return _isPaused; } }



        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _isPaused = !_isPaused;
            FrameInputEvent(new FrameInputData(Input.mousePosition, _isPaused));
        }
    }
}