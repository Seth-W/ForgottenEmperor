namespace CFE.Actions.EntityActions
{
    using UnityEngine;
    using Extensions;
    using System;

    class MoveAction : IAction
    {
        int pathingIndex;
        float pathingInterpolator;
        Vector3[] pathingWaypoints;
        int pathingStepsToSkip;

        TilePosition endPos;
        Transform entityTransform;
        EntityModel model;

        public TilePosition cancelPos
        {
            get
            {
                if (pathingIndex + 1 < pathingWaypoints.Length)
                    return new TilePosition(pathingWaypoints[pathingIndex + 1]);
                else
                    return TilePosition.nullTPos;
            }
        }

        bool firstExecute, cancelFirstCall;

        public bool isNullAction { get { return pathingWaypoints.Length <= 1; } }

        public MoveAction(Transform entityToMove, EntityModel entityModel, TilePosition endPos)
        {
            pathingIndex = 0;
            pathingInterpolator = 0;
            pathingStepsToSkip = 0;
            pathingWaypoints = new Vector3[1];

            entityTransform = entityToMove;
            model = entityModel;
            this.endPos = endPos;

            firstExecute = true;
            cancelFirstCall = true;
        }

        public MoveAction(Transform entityToMove, EntityModel entityModel, TilePosition endPos, int stepsToSkip)
        {
            pathingIndex = 0;
            pathingInterpolator = 0;
            pathingStepsToSkip = stepsToSkip;

            entityTransform = entityToMove;
            model = entityModel;
            this.endPos = endPos;

            firstExecute = true;
            cancelFirstCall = true;
        }


        public bool execute()
        {
            if (firstExecute)
            {
                firstExecute = false;
                if (pathingStepsToSkip == 0)
                    pathingWaypoints = Pathfinder.findPath(new TilePosition(entityTransform.position), endPos).ToVector3Array();
                else
                    pathingWaypoints = Pathfinder.findPathIgnoreEndTilePathfindingEnabled(new TilePosition(entityTransform.position), endPos).ToVector3Array();
                if (pathingWaypoints.Length > 1)
                {
                    TileModel firstTile = TileManager.getTile(new TilePosition(pathingWaypoints[1]));
                    if (firstTile.getPathFindingEnabled())
                        firstTile.updateEntity(model);
                    else
                        correctForBlockedPath();
                }
            }

            if (pathingIndex >= pathingWaypoints.Length - (1 + pathingStepsToSkip))
            {
                if(pathingIndex + 1 < pathingWaypoints.Length)
                    TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex + 1])).updateEntity(null);
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
                TileManager.getTile(new TilePosition(pathingWaypoints[1])).updateEntity(model);
            }
            if (pathingInterpolator < 1f)
            {
                entityTransform.position = Vector3.Lerp(pathingWaypoints[pathingIndex], pathingWaypoints[pathingIndex + 1], pathingInterpolator);
                pathingInterpolator += Time.deltaTime * 10 * model.speed;
                return false;
            }
            entityTransform.position = pathingWaypoints[1];
            model.control.setCurrentFullPathEndPos(new TilePosition(pathingWaypoints[1]));
            TileManager.getTile(new TilePosition(pathingWaypoints[0])).updateEntity(null);
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
