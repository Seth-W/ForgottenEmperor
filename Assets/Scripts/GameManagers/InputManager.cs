namespace CFE
{

    using UnityEngine;
    using System.Collections;

    class InputManager : MonoBehaviour
    {
        public delegate void FrameInput(FrameInputData data);
        public static FrameInput FrameInputEvent;

		public delegate void PauseStatus(bool status);
		public static PauseStatus PauseEvent;

        static bool _isPaused = false;

        public static bool isPaused { get { return _isPaused; } }
        public static FrameInputData currentFrameInputData;

        #region
        void Update()
        {
			if (Input.GetKeyDown (KeyCode.Space))
			{
				_isPaused = !_isPaused;
				PauseEvent (_isPaused);
			}
            FrameInputEvent(new FrameInputData(Input.mousePosition, _isPaused));
        }

        void OnEnable()
        {
            FrameInputEvent += OnFrameInputEvent;
        }

        void OnDisable()
        {
            FrameInputEvent -= OnFrameInputEvent;
        }
        #endregion

        private void OnFrameInputEvent(FrameInputData data)
        {
            currentFrameInputData = data;
        }
    }
}