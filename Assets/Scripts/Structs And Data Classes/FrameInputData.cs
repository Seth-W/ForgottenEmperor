namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class FrameInputData
    {
        Vector3 _mousePositionScreen, _mousePositionWorld;
        public Vector3 mousePositionScreen { get { return _mousePositionScreen; } }
        public Vector3 mousePositionWorld { get { return _mousePositionWorld; } }

        MouseInputData _mouseData;
        public MouseInputData mouseData { get { return _mouseData; } }

        public TilePosition tilePos;

        public FrameInputData(Vector3 screenPos)
        {
            _mouseData = new MouseInputData();

            _mousePositionScreen = screenPos;
            _mousePositionWorld = Camera.main.ScreenToWorldPoint(screenPos);
            tilePos = new TilePosition(_mousePositionWorld);
        }
    }
}