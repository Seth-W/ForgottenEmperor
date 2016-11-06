namespace CFE
{
    using UnityEngine;

    class TileModel
    {
        TilePosition tilePos;
        bool pathingEnabled;
        EntityModel model;
        public EntityModel Model { get { return model; } }


        public TileModel(int xIndex, int yIndex, bool pathingEnabled)
        {
            tilePos = new TilePosition(xIndex, yIndex);
            this.pathingEnabled = pathingEnabled;
            model = null;
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
    }
}
