namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;

    class TileModel
    {
        TilePosition tilePos;
        bool pathingEnabled;

        EntityModel model;
        public EntityModel Model { get { return model; } }

        Dictionary<TileEffectType, ITileEffect> tileEffects;

        public TileModel(int xIndex, int yIndex, bool pathingEnabled)
        {
            tilePos = new TilePosition(xIndex, yIndex);
            this.pathingEnabled = pathingEnabled;
            model = null;
            tileEffects = new Dictionary<TileEffectType, ITileEffect>();

        }

        /**
        *<summary>
        *Returns true if pathfinding is enabled for this tile and the tile is unoccupied 
        *</summary>
        */
        public bool getPathFindingEnabled()
        {
            return pathingEnabled && model == null;
        }

        /**
        *<summary>
        *Stores the given <see cref="EntityModel"/> in the model field
        *</summary>
        */
        public void updateEntity(EntityModel newModel)
        {
            model = newModel;
        }

        /**
        *<summary>
        *Registers an <see cref="ITileEffect"/> to the dictionary of TileEffects with a <see cref="TileEffectType"/> as the key
        *</summary>
        */
        public bool registerTileEffect(TileEffectType key, ITileEffect effect)
        {
            if (!tileEffects.ContainsKey(key))
            {
                tileEffects.Add(key, effect);
                return true;
            }
            return false;
        }

        /**
        *<summary>
        *Deregisters the <see cref="ITileEffect"/> corresponding to the given key from the list of TileEffects
        *</summary>
        */
        public void deregisterTileEffect(TileEffectType key)
        {
            if (tileEffects.ContainsKey(key))
                tileEffects.Remove(key);
        }

        public void OnTickUpdate(Tick data)
        {
            List<ITileEffect> effects = new List<ITileEffect>();

            foreach(KeyValuePair<TileEffectType, ITileEffect> keyVal in tileEffects)
            {
                effects.Add(keyVal.Value);
            }

            foreach (ITileEffect item in effects)
            {
                item.OnTickUpdate(data);
            }
        }
    }
}
