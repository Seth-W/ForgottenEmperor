namespace CFE
{

    using UnityEngine;
    using System.Collections;

    class MouseInputData
    {
        bool _mouse0, _mouse0Down, _mouse0Up, _mouse1, _mouse1Down, _mouse1Up;

        public bool mouse0 { get { return _mouse0; } }
        public bool mouse0Down { get { return _mouse0Down; } }
        public bool mouse0Up { get { return _mouse0Up; } }
        public bool mouse1 { get { return _mouse1; } }
        public bool mouse1Down { get { return _mouse1Down; } }
        public bool mouse1up { get { return _mouse1Up; } }

        public MouseInputData()
        {
            _mouse0 = Input.GetMouseButton(0);
            _mouse0Down = Input.GetMouseButtonDown(0);
            _mouse0Up = Input.GetMouseButtonUp(0);
            _mouse1 = Input.GetMouseButton(1);
            _mouse1Down = Input.GetMouseButtonDown(1);
            _mouse1Up = Input.GetMouseButtonUp(1);
        }
    }
}