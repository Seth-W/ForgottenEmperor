namespace CFE
{
    using System;
    using UnityEngine;
    using Extensions;

    class RadialSpawnProjectile : Projectile, IProjectile
    {
        int radius;
        
        void OnDisable()
        {
            TickManager.TickUpdateEvent -= OnTickUpdate;
        }

        public override void Initiate(TilePosition startPos, TilePosition endPos, StatBlock casterStats)
        {
            base.Initiate(startPos, endPos, casterStats);
            radius = AbilityManager.getAbilityData(abilityKey).radius;
            this.casterStats = casterStats;
        }

        public override void Terminate()
        {
            CastRadialAbility(new TilePosition(transform.position));
            Destroy(gameObject);
        }

        private void CastRadialAbility(TilePosition target)
        {
            Vector2[] tilePositions = Vector2Helper.getRadial(radius);

            for (int i = 0; i < tilePositions.Length; i++)
            {
                tilePositions[i] += target.tilePosition;
                GameObject obj = Instantiate(tileEffectPrefab, tilePositions[i], Quaternion.identity) as GameObject;
                obj.GetComponent<ITileEffect>().Initialize(casterStats);
            }
        }
    }
}
