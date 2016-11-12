namespace CFE.Actions.EntityActions
{
    using UnityEngine;
    using MethodExtensions;
    using System;

    class MoveAction : IAction
    {
        int pathingIndex;
        float pathingInterpolator;
        Vector3[] pathingWaypoints;
        Transform entityTransform;
        EntityModel model;

        bool firstExecute, cancelFirstCall;

        public bool isNullAction { get { return pathingWaypoints.Length <= 1; } }

        public MoveAction(Vector3[] waypoint, float interpolator, Transform entityToMove, EntityModel entityModel)
        {
            pathingIndex = 0;
            pathingInterpolator = interpolator;
            pathingWaypoints = waypoint;
            entityTransform = entityToMove;
            model = entityModel;
            firstExecute = true;
            cancelFirstCall = true;
        }

        public bool execute()
        {
            if (firstExecute)
            {
                model.isMoving = true;
                firstExecute = false;
                if (pathingWaypoints.Length > 1)
                {
                    TileModel firstTile = TileManager.getTile(new TilePosition(pathingWaypoints[1]));
                    if (firstTile.getPathFindingEnabled())
                        firstTile.updateEntity(model);
                    else
                        correctForBlockedPath();
                }
            }

            if (pathingIndex >= pathingWaypoints.Length - 1)
            {
                model.isMoving = false;
                return true; 
            }
            if (pathingInterpolator < 1f)
            {
                entityTransform.position = Vector3.Lerp(pathingWaypoints[pathingIndex], pathingWaypoints[pathingIndex + 1], pathingInterpolator);
                pathingInterpolator += Time.deltaTime * 10 * model.speed;
                return false;
            }

            incrementWaypointIndex();

            if (pathingWaypoints.Length > pathingIndex + 1)
            {
                TileModel nextTile = TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex + 1]));

                if (!nextTile.getPathFindingEnabled())
                {
                    correctForBlockedPath();
                    return false;
                }
                nextTile.updateEntity(model);
            }
            TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex])).updateEntity(model);
            return false;
        }

        public bool revert()
        {
            return true;
        }

        public bool cancel()
        {
            if (cancelFirstCall)
            {
                cancelFirstCall = false;
                pathingWaypoints = Pathfinder.findPath(new TilePosition(pathingWaypoints[pathingIndex]), new TilePosition(pathingWaypoints[pathingIndex + 1])).ToVector3Array();
                pathingIndex = 0;
                pathingInterpolator = getAdjustedInterpolator(pathingIndex);
            }
            if(pathingInterpolator < 1f)
            {
                entityTransform.position = Vector3.Lerp(pathingWaypoints[pathingIndex], pathingWaypoints[pathingIndex + 1], pathingInterpolator);
                pathingInterpolator += Time.deltaTime * 10 * model.speed;
                return false;
            }
            model.isMoving = false;
            entityTransform.position = pathingWaypoints[1];
            TileManager.getTile(new TilePosition(pathingWaypoints[0])).updateEntity(null);
            TileManager.getTile(new TilePosition(pathingWaypoints[1])).updateEntity(null);
            return true;
        }

        /**
        *<summary>
        *Updates the fields for interpolating along waypoints when an entity has reached the end of a path between two tiles
        *</summary>
        */
        private void incrementWaypointIndex()
        {
            entityTransform.position = pathingWaypoints[pathingIndex + 1];
            pathingIndex++;
            pathingInterpolator = 0f;
            TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex - 1])).updateEntity(null);
        }

        private void correctForBlockedPath()
        {
            Debug.Log("Pathing was interrupted");
            pathingWaypoints = Pathfinder.findPath(new TilePosition(pathingWaypoints[pathingIndex]), new TilePosition(pathingWaypoints[pathingWaypoints.Length - 1])).ToVector3Array();
            pathingIndex = 0;
            TileManager.getTile(new TilePosition(pathingWaypoints[0])).updateEntity(model);
            TileManager.getTile(new TilePosition(pathingWaypoints[1])).updateEntity(model);
        }

        private float getAdjustedInterpolator(int index)
        {
            float retValue = 0;
            Vector3 entityPos = model.transform.position;
            if (pathingWaypoints.Length > index + 1)
            {
                retValue = Mathf.Abs(entityPos.y - pathingWaypoints[index + 1].y);
                retValue += Mathf.Abs(entityPos.x - pathingWaypoints[index + 1].x);
            }
            else
            {
                retValue = 0f;
            }

            return 1 - retValue;
        }
    }
}
