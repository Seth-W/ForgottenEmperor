namespace CFE
{
    using UnityEngine;

    class DebugDisplayManager : MonoBehaviour
    {
        [SerializeField]
        GameObject tileIndexDebugDisplayPrefab, tileHeuristicDisplayPrefab;

        void Start()
        {
            for (int i = 0; i < DataManager.Width; i++)
            {
                for (int j = 0; j < DataManager.Height; j++)
                {
                    Instantiate(tileIndexDebugDisplayPrefab, new TilePosition(i, j).tilePosition, Quaternion.identity);
                    Instantiate(tileHeuristicDisplayPrefab, new TilePosition(i, j).tilePosition, Quaternion.identity);

                }
            }
        }
    }
}
