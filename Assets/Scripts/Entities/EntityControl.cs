namespace CFE
{
    using UnityEngine;
    using MethodExtensions;


    class EntityControl : MonoBehaviour
    {
        protected EntityModel model;
        bool _isMoving;

        int pathingIndex = 0;
        float pathingInterpolator = 0;
        Vector3[] pathingWaypoints;

        void Start()
        {
            pathingWaypoints = new Vector3[0];
        }

        void OnEnable()
        {
            model = GetComponent<EntityModel>();

            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnFrameInput;
        }

        void OnFrameInput(FrameInputData data)
        {
            /**
            if (data.mouseData.mouse0down)
            {
                moveTo(data.tilePos);
            }
            **/
            
            if(data.mouseData.mouse0Down)
            {
                moveEntity(data.tilePos);
                _isMoving = true;
            }
            if(_isMoving && !data.isPaused)
                FollowPath(pathingWaypoints, pathingIndex, pathingInterpolator);
        }


        /**
        *<summary>
        *Moves the entity to the given <see cref="TilePosition"/>
        *</summary>
        */
        public void moveEntity(TilePosition endPos)
        {
            pathingIndex = 0;

            if (_isMoving)
            {
                pathingWaypoints = Pathfinder.findPath(new TilePosition(transform.position), endPos).ToVector3Array();
                pathingWaypoints[0] = transform.position;
                pathingInterpolator = 1 - (Vector3.Distance(pathingWaypoints[0], pathingWaypoints[1]));
            }
            else
            {
                pathingWaypoints = Pathfinder.findPath(new TilePosition(transform.position), endPos).ToVector3Array();
                pathingInterpolator = 0f;
            }
        }

        void FollowPath(Vector3[] waypoints, int index, float interpolator)
        {
            if (index >= waypoints.Length - 1)
            {
                _isMoving = false;
                return;
            }
            if (interpolator < 1f)
            {
                transform.position = Vector3.Lerp(waypoints[index], waypoints[index + 1], interpolator);
                pathingInterpolator += Time.deltaTime * 10 * model.speed;
                return;
            }
            transform.position = waypoints[index + 1];
            pathingIndex++;
            pathingInterpolator = 0f;
        }

        float getLengthAdjustedInterpolateValue(Vector3[] path, int index)
        {
            float xDis, yDis;
            xDis = Mathf.Abs(path[index].x - path[index + 1].x);
            yDis = Mathf.Abs(path[index].y - path[index + 1].y);

            return xDis + yDis;
        }
    }
}