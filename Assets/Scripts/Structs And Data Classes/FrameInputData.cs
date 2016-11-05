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

        public bool isPaused, inPlayableArea;

        public FrameInputData(Vector3 screenPos, bool paused)
        {
            _mouseData = new MouseInputData();

            mousePositionScreen = screenPos;
            mousePositionWorld = Camera.main.ScreenToWorldPoint(screenPos);
            tilePos = new TilePosition(mousePositionWorld);
            isPaused = paused;
            inPlayableArea = getInPlayableArea();
        }

        /**
        *<summary>
        *If the mouse world position is outside the play area, returns false.
        *Prevents mouse input being read when player is using UI1
        *</summary>
        */
        private bool getInPlayableArea()
        {
            bool leftBounded = mousePositionWorld.x < -DataManager.Width / 2;
            bool rightBounded = mousePositionWorld.x > DataManager.Width / 2;
            bool lowerBounded = mousePositionWorld.y < -DataManager.Height / 2;
            bool upperBounded = mousePositionWorld.y > DataManager.Height / 2;

            return !(leftBounded || rightBounded || lowerBounded || upperBounded);
        }
    }
}