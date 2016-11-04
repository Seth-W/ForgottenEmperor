namespace CFE
{

    using UnityEngine;
    using System.Collections;

    class InputManager : MonoBehaviour
    {
        public delegate void FrameInput(FrameInputData data);

        public static FrameInput FrameInputEvent;

        void Update()
        {
            FrameInputEvent(new FrameInputData(Input.mousePosition));
        }
    }
}