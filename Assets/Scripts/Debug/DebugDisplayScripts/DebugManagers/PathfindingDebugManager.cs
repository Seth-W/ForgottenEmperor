namespace CFE
{
    using UnityEngine;
    using Extensions;

    class PathfindingDebugManager : MonoBehaviour
    {
        TilePosition lastFramePos;
        LineRenderer line;

        void Start()
        {
            lastFramePos = default(TilePosition);
            line = GetComponent<LineRenderer>();
        }

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
            if(lastFramePos != data.tilePos)
            {
                lastFramePos = data.tilePos;
                Vector3[] waypoints = Pathfinder.findPath(new TilePosition(Vector3.zero), data.tilePos).ToVector3Array();
                for (int i = 0; i < waypoints.Length; i++)
                {
                    waypoints[i].z = -0.1f;
                }
                line.SetVertexCount(waypoints.Length);
                line.SetPositions(waypoints);
            }
        }
    }
}