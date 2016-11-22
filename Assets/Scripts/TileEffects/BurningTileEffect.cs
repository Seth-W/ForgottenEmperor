using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFE
{
    using UnityEngine;

    class BurningTileEffect : MonoBehaviour, ITileEffect
    {
        [SerializeField]
        TileEffectType type;
        [SerializeField]
        DamageType damageType;
        [SerializeField]
        int duration;
        int timeSinceInit;
        [SerializeField]
        int damage;

        void OnDisable()
        {
            TickManager.TickUpdateEvent -= OnTickUpdate;
        }

        public void Initialize(StatBlock castingEntityStats)
        {
            damage += castingEntityStats.intelligence;
            timeSinceInit = 0;
            //TickManager.TickUpdateEvent += OnTickUpdate;
            if(!TileManager.getTile(new TilePosition(transform.position)).registerTileEffect(type, this))
            {
                Destroy(gameObject);
            }
        }

        public void OnTickUpdate(Tick data)
        {
            timeSinceInit += 1;
            if (timeSinceInit > duration)
                Terminate();
            if (timeSinceInit % TickManager.TicksPerSecond == 0)
            {
                EntityModel model = TileManager.getTile(new TilePosition(transform.position)).Model;
                if (model != null)
                {
                    EResource resourceComponent = model.GetComponent<EResource>();
                    if (resourceComponent != null)
                    {
                        resourceComponent.IncrementHealth(-damage, damageType);
                    }
                }
            }
        }

        public void Terminate()
        {
            //TickManager.TickUpdateEvent -= OnTickUpdate;
            TileManager.getTile(new TilePosition(transform.position)).deregisterTileEffect(type);
            Destroy(gameObject);
        }

    }
}
