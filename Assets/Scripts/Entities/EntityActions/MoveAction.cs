namespace CFE.Actions.EntityActions
{
    using UnityEngine;
    using MethodExtensions;

    class MoveAction : IAction
    {
        int pathingIndex;
        float pathingInterpolator;
        Vector3[] pathingWaypoints;
        Transform entityTransform;
        EntityModel model;

        public MoveAction(Vector3[] waypoint, float interpolator, Transform entityToMove, EntityModel entityModel)
        {
            pathingIndex = 0;
            pathingInterpolator = interpolator;
            pathingWaypoints = waypoint;
            entityTransform = entityToMove;
            model = entityModel;
        }

        public bool execute()
        {
            model.isMoving = true;
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
            entityTransform.position = pathingWaypoints[pathingIndex + 1];
            pathingIndex++;
            pathingInterpolator = 0f;
            TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex - 1])).updateEntity(null);
            if (pathingWaypoints.Length > pathingIndex + 1)
            {
                TileModel nextTile = TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex + 1]));
                if (!nextTile.getPathFindingEnabled())
                {
                    Debug.Log("Pathing was interrupted");
                    pathingWaypoints = Pathfinder.findPath(new TilePosition(pathingWaypoints[pathingIndex]), new TilePosition(pathingWaypoints[pathingWaypoints.Length - 1])).ToVector3Array();
                    pathingIndex = 0;
                    TileManager.getTile(new TilePosition(pathingWaypoints[0])).updateEntity(model);
                    return false;
                }
            }
            TileManager.getTile(new TilePosition(pathingWaypoints[pathingIndex ])).updateEntity(model);
            return false;
        }

        public bool revert()
        {
            return true;
        }
    }
}
