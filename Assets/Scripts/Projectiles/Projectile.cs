namespace CFE
{
    using UnityEngine;
    using Extensions;

    class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField, Range(0, 0.5f)]
        float tolerance;


        [SerializeField]
        protected float speed;
        [SerializeField]
        protected string abilityKey;


        protected GameObject tileEffectPrefab;


        protected TilePosition startPos, endPos;
        Vector2 dir, totalDistance, distanceSoFar;

        void OnDisable()
        {
            TickManager.TickUpdateEvent -= OnTickUpdate;
        }

        public void Initiate(TilePosition startPos, Transform target)
        {

        }

        public virtual void Initiate(TilePosition startPos, TilePosition endPos)
        {
            TickManager.TickUpdateEvent += OnTickUpdate;

            this.startPos = startPos;
            this.endPos = endPos;

            totalDistance = endPos.tilePosition - startPos.tilePosition;
            dir = totalDistance.normalized;
            distanceSoFar = new Vector2(0, 0);

            tileEffectPrefab = AbilityManager.getAbilityData(abilityKey).tileEffectPrefab;
        }

        public virtual void OnTickUpdate(Tick tickData)
        {
            while (distanceSoFar.magnitude < totalDistance.magnitude)
            {
                transform.Translate(dir * speed * Tick.DeltaTime);
                distanceSoFar += dir * speed * Tick.DeltaTime;
                return;
            }
            transform.position = endPos.tilePosition;
            Terminate();
        }

        public virtual void Terminate()
        {
            Destroy(gameObject);
        }

        protected bool checkArrived(TilePosition endPos, Transform projectilePos, Vector2 movementVector)
        {
            return (endPos.tilePosition - (Vector2)projectilePos.position).magnitude < tolerance;
        }
    }
}