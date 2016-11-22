namespace CFE
{
    using UnityEngine;

    class TileManager : MonoBehaviour
    {
        TileModel[,] _tileMap;
        static TileModel[,] tileMap;

        void Start()
        {
            init();
        }

        void OnEnable()
        {
            TickManager.TickUpdateEvent += OnTickUpdate;
        }

        void OnDisable()
        {
            TickManager.TickUpdateEvent -= OnTickUpdate;
        }

        /**
        *<summary>
        *Returns the <see cref="TileModel"/> at the given indices
        *</summary>
        */
        public static TileModel getTile(int xIndex, int yIndex)
        {
            return tileMap[xIndex, yIndex];
        }

        /**
        *<summary>
        *Returns the <see cref="TileModel"/> for the given <see cref="TilePosition"/>
        *</summary>
        */
        public static TileModel getTile(TilePosition tilePos)
        {
            return tileMap[tilePos.xIndex, tilePos.yIndex];
        }

        private void OnTickUpdate(Tick data)
        {
            foreach (TileModel item in _tileMap)
            {
                item.OnTickUpdate(data);
            }
        }

        void init()
        {
            if (tileMap != null)
            {
                Debug.LogError("Multiple instances of a tile manager exist in this scene");
                Destroy(this);
            }
            _tileMap = new TileModel[DataManager.Width + 1, DataManager.Height + 1];

            for (int i = 0; i < DataManager.Width + 1; i++)
            {
                for (int j = 0; j < DataManager.Height + 1; j++)
                {
                    bool enabled = Random.value < 0.25f;
                    _tileMap[i, j] = new TileModel(i, j, true);
                }
            }

            tileMap = _tileMap;
        }
    }
}
