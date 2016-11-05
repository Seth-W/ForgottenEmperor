namespace CFE
{
    using UnityEngine;

    class DebugTileHeuristicDisplay : MonoBehaviour, IDebugDisplay
    {
        TextMesh text;
        TilePosition tilePos;
        MeshRenderer rend;

        void Start()
        {
            text = GetComponent<TextMesh>();
            rend = GetComponent<MeshRenderer>();

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

        public void Display(bool b)
        {
            rend.enabled = b;
        }
    }
}