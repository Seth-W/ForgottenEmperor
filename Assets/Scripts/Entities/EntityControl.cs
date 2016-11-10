namespace CFE
{
    using UnityEngine;
    using MethodExtensions;
    using Actions.EntityActions;


    class EntityControl : MonoBehaviour
    {
        protected EntityModel model;

        /// <summary>
        /// The end position for all the Queued Move Actions
        /// </summary>
        TilePosition currentFullPathEndPos;


        void Start()
        {
        }

        void OnEnable()
        {
            model = GetComponent<EntityModel>();
        }

        void OnDisable()
        {
        }

        public void OnFrameInput(FrameInputData data)
        {
            if (data.mouseData.mouse0Down && data.inPlayableArea)
            {
                Move(data.tilePos, Input.GetKey(KeyCode.LeftShift));
            }
        }

        /**
        *<summary>
        *Queues a <see cref="MoveAction"/> for this tile
        *<para>If <paramref name="queueAction"/> is false, simply runs the action instead</para>
        *</summary>
        */
        private void Move(TilePosition destination, bool queueAction)
        {
            if (queueAction)
                model.enqueueAction(getMoveAction(currentFullPathEndPos, destination, true));
            else
            {
                MoveAction moveAction = getMoveAction(new TilePosition(transform.position), destination, false);
                //if(!moveAction.isNullAction)
                    model.runAction(moveAction);
            }
        }

        /**
        *<summary>
        *Creates a <see cref="MoveAction"/> for the current entity from a starting <see cref="TilePosition"/> to another <see cref="TilePosition"/>
        *</summary>
        */
        private MoveAction getMoveAction(TilePosition startPos, TilePosition endPos, bool isActionQueued)
        {
            float pathingInterpolator = 0;
            Vector3[] pathingWaypoints;

            if (model.isMoving && !isActionQueued)
            {
                pathingWaypoints = Pathfinder.findPath(startPos, endPos).ToVector3Array();
                if (pathingWaypoints.Length > 0)
                {
                    pathingWaypoints[0] = transform.position;
                    if (pathingWaypoints.Length > 1)
                        pathingInterpolator = 1 - (Vector3.Distance(pathingWaypoints[0], pathingWaypoints[1]));
                }
            }
            else
            {
                pathingWaypoints = Pathfinder.findPath(startPos, endPos).ToVector3Array();
                pathingInterpolator = 0f;
            }
            currentFullPathEndPos = endPos;
            return new MoveAction(pathingWaypoints, pathingInterpolator, transform, model);
        }
    }
}