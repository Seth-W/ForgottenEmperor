namespace CFE.Actions.EntityActions
{
    using UnityEngine;

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
            return false;
        }

        public bool revert()
        {
            return true;
        }
    }
}
