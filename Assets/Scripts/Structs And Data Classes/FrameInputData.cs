namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class FrameInputData
    {
        public Vector3 mousePositionScreen, mousePositionWorld;

        MouseInputData _mouseData;
        public MouseInputData mouseData { get { return _mouseData; } }

        public TilePosition tilePos;

        public bool isPaused;

        public FrameInputData(Vector3 screenPos, bool paused)
        {
            _mouseData = new MouseInputData();

            mousePositionScreen = screenPos;
            mousePositionWorld = Camera.main.ScreenToWorldPoint(screenPos);
            tilePos = new TilePosition(mousePositionWorld);
            isPaused = paused;
        }
    }
}