namespace CFE
{
    using UnityEngine;

    class Wall : MonoBehaviour
    {
        #region MonoBehaviorus
        void Start()
        {
            int xSize, ySize;
            TilePosition tilePos = new TilePosition(transform.position);
            xSize = (int)transform.localScale.x;
            ySize = (int)transform.localScale.y;
            if(xSize > ySize)
            {
                for (int i = 0; i < xSize; i++)
                {
                    TileManager.getTile(tilePos.xIndex + (i - xSize / 2), tilePos.yIndex).setPathFindingEnabled(false);
                }
            }
            else if (xSize < ySize)
            {
                for (int i = 0; i < ySize; i++)
                {
                    TileManager.getTile(tilePos.xIndex, tilePos.yIndex + (i - xSize / 2)).setPathFindingEnabled(false);
                }
            }
        }
        #endregion
    }
}
