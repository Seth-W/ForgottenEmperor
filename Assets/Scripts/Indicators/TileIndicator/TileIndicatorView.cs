namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class TileIndicatorView : MonoBehaviour
    {

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
            setPosition(data.tilePos);
        }

        /**
        *<summary>
        *Moves the game object to a given <see cref="Vector3"/>
        *</summary>
        */
        public void setPosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        /**
        *<summary>
        *Moves the game object to a given <see cref="TilePosition"/>
        *</summary>
        */
        public void setPosition(TilePosition tilePos)
        {
            transform.position = tilePos.tilePosition;
        }
    }
}