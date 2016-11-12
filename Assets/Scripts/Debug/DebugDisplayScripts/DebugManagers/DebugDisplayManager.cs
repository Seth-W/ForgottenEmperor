namespace CFE
{
    using UnityEngine;

    class DebugDisplayManager : MonoBehaviour
    {
        [SerializeField]
        bool heuristicDisplayed, indexDisplayed, entityDisplayed;
        bool _heuristicDisplayed, _indexDisplayed, _entityDisplayed;

        [SerializeField]
        GameObject tileIndexDebugDisplayPrefab, tileHeuristicDisplayPrefab, tileEntityDisplayPrefab;
        IDebugDisplay[,] heuristicDisplays;
        IDebugDisplay[,] indexDisplays;
        IDebugDisplay[,] entityDisplays;

        void Start()
        {
            heuristicDisplays = new DebugTileHeuristicDisplay[DataManager.Width, DataManager.Height];
            indexDisplays = new DebugTileIndexDisplay[DataManager.Width, DataManager.Height];
            entityDisplays = new DebugTileEntityDisplay[DataManager.Width, DataManager.Height];

            _heuristicDisplayed = true;
            _indexDisplayed = true;
            _entityDisplayed = true;


            for (int i = 0; i < DataManager.Width; i++)
            {
                for (int j = 0; j < DataManager.Height; j++)
                {
                    GameObject temp;
                    TilePosition tilePos = new TilePosition(i, j);
                    temp = Instantiate(tileIndexDebugDisplayPrefab, tilePos.tilePosition, Quaternion.identity) as GameObject;
                    temp.transform.parent = transform;
                    indexDisplays[i, j] = temp.GetComponent<DebugTileIndexDisplay>();

                    temp = Instantiate(tileHeuristicDisplayPrefab, tilePos.tilePosition, Quaternion.identity) as GameObject;
                    temp.transform.parent = transform;
                    heuristicDisplays[i, j] = temp.GetComponent<DebugTileHeuristicDisplay>();

                    temp = Instantiate(tileEntityDisplayPrefab, tilePos.tilePosition, Quaternion.identity) as GameObject;
                    temp.transform.parent = transform;
                    entityDisplays[i, j] = temp.GetComponent<DebugTileEntityDisplay>();
                }
            }
        }

        void Update()
        {
            if(heuristicDisplayed != _heuristicDisplayed)
            {
                setDebugActive(heuristicDisplays, heuristicDisplayed);
                _heuristicDisplayed = heuristicDisplayed;
            }
            if(indexDisplayed != _indexDisplayed)
            {
                setDebugActive(indexDisplays, indexDisplayed);
                _indexDisplayed = indexDisplayed;
            }
            if(entityDisplayed != _entityDisplayed)
            {
                setDebugActive(entityDisplays, entityDisplayed);
                _entityDisplayed = entityDisplayed;
            }
        }

        void setDebugActive(IDebugDisplay[,] debugs, bool b)
        {
            for (int i = 0; i < DataManager.Width; i++)
            {
                for (int j = 0; j < DataManager.Height; j++)
                {
                    debugs[i, j].Display(b);
                }
            }
        }
        
    }
}
