namespace CFE
{
    using UnityEngine;

    class DebugDisplayManager : MonoBehaviour
    {
        [SerializeField]
        bool heuristicDisplayed, indexDisplayed;
        bool _heuristicDisplayed, _indexDisplayed;

        [SerializeField]
        GameObject tileIndexDebugDisplayPrefab, tileHeuristicDisplayPrefab;
        IDebugDisplay[,] heuristicDisplays;
        IDebugDisplay[,] indexDisplays;

        void Start()
        {
            heuristicDisplays = new DebugTileHeuristicDisplay[DataManager.Width, DataManager.Height];
            indexDisplays = new DebugTileIndexDisplay[DataManager.Width, DataManager.Height];

            _heuristicDisplayed = true;
            _indexDisplayed = true;


            for (int i = 0; i < DataManager.Width; i++)
            {
                for (int j = 0; j < DataManager.Height; j++)
                {
                    GameObject temp;
                    temp = Instantiate(tileIndexDebugDisplayPrefab, new TilePosition(i, j).tilePosition, Quaternion.identity) as GameObject;
                    temp.transform.parent = transform;
                    indexDisplays[i, j] = temp.GetComponent<DebugTileIndexDisplay>();

                    temp = Instantiate(tileHeuristicDisplayPrefab, new TilePosition(i, j).tilePosition, Quaternion.identity) as GameObject;
                    temp.transform.parent = transform;
                    heuristicDisplays[i, j] = temp.GetComponent<DebugTileHeuristicDisplay>();
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
