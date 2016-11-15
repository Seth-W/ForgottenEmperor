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
        int duration;
        int timeSinceInit;
        [SerializeField]
        int damage;

        void OnDisable()
        {
            TickManager.TickUpdateEvent -= OnTickUpdate;
        }

        public void Initialize()
        {
            timeSinceInit = 0;
            TickManager.TickUpdateEvent += OnTickUpdate;
        }

        public void OnTickUpdate(Tick data)
        {
            timeSinceInit += 1;
            if (timeSinceInit > duration)
                Terminate();
            if (timeSinceInit % TickManager.TicksPerSecond == 0)
            {
                EntityModel modelOnTile = TileManager.getTile(new TilePosition(transform.position)).Model;
                if(modelOnTile != null)
                    Debug.Log("Dealing " + damage + " damage to: " + modelOnTile);
            }
        }

        public void Terminate()
        {
            Debug.Log("Terminating burning tile effect");
            TickManager.TickUpdateEvent -= OnTickUpdate;
            Destroy(gameObject);
        }

    }
}
