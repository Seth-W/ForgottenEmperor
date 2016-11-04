namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class MousePositionIndicatorView : MonoBehaviour
    {
        [SerializeField]
        float zOffset;

        void OnEnable()
        {
            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnFrameInput;
        }

        void OnFrameInput(FrameInputData data)
        {
            setPosition(data.mousePositionWorld);
        }

        /**
        *<summary>
        *Moves the game object to a given <see cref="Vector3"/>
        *</summary>
        */
        void setPosition(Vector3 newPos)
        {
            newPos.z = zOffset;
            transform.position = newPos;
        }
    }
}