namespace CFE
{
    using UnityEngine;
    using MethodExtensions;

    class PathfindingDebugManager : MonoBehaviour
    {
        TilePosition lastFrameEndPos, lastFrameStartPos;
        LineRenderer line;

        void Start()
        {
            lastFrameEndPos = default(TilePosition);
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
            TilePosition startPos = new TilePosition(transform.position);
            if (lastFrameEndPos != data.tilePos || lastFrameStartPos != startPos)
            {
                lastFrameEndPos = data.tilePos;
                Vector3[] waypoints = Pathfinder.findPath(startPos, data.tilePos).ToVector3Array();
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