namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class DebugTileHeuristicDisplay : MonoBehaviour
    {
        TextMesh text;
        TilePosition tilePos;

        void Start()
        {
            text = GetComponent<TextMesh>();
            text.text = 0.ToString();
            tilePos = new TilePosition(transform.position);
            transform.Translate(new Vector3(0, 0, -0.2f));
        }

        void OnEnable()
        {
            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnDisable()
        {
            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnFrameInput(FrameInputData data)
        {
            text.text = Pathfinder.heuristicDistance(tilePos.xIndex, tilePos.yIndex, data.tilePos.xIndex, data.tilePos.yIndex).ToString();
        }
    }
}