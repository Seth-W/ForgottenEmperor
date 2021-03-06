﻿namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class FrameInputData
    {
        public Vector3 mousePositionScreen, mousePositionWorld;

        MouseInputData _mouseData;
        public MouseInputData mouseData { get { return _mouseData; } }

        public TilePosition tilePos, activeEntityTilePos;

        public bool isPaused, inPlayableArea;

        public FrameInputData(Vector3 screenPos, bool paused)
        {
            _mouseData = new MouseInputData();

            mousePositionScreen = screenPos;
            mousePositionWorld = Camera.main.ScreenToWorldPoint(screenPos);
            tilePos = new TilePosition(mousePositionWorld);
            activeEntityTilePos = new TilePosition(EntityManager.activePlayer.transform.position);
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
            bool leftBounded = mousePositionWorld.x < -DataManager.Width / 2 - 0.5f;
            bool rightBounded = mousePositionWorld.x > DataManager.Width / 2 + 0.5f;
            bool lowerBounded = mousePositionWorld.y < -DataManager.Height / 2 - 0.5f;
            bool upperBounded = mousePositionWorld.y > DataManager.Height / 2 + 0.5f;

            return !(leftBounded || rightBounded || lowerBounded || upperBounded);
        }
    }
}