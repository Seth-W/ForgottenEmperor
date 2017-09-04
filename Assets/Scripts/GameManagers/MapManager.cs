namespace CFE
{
    using UnityEngine;

    class MapManager : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(DataManager.Width);
            Debug.Log(DataManager.Height);
            instantiateGameBoard(DataManager.Width, DataManager.Height);
        }

        private void instantiateGameBoard(int xIndex, int yIndex)
        {

            for (int i = 0; i < xIndex; i++)
            {
                for (int j = 0; j < yIndex; j++)
                {
                    Debug.Log(i + ',' + j);
                    instantiateBoardTile(new TilePosition(i, j));
                }
            }
        }

        private void instantiateBoardTile(TilePosition tilePos)
        {

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Vector3 newPos = new Vector3(tilePos.tilePosition.x, tilePos.tilePosition.y, -2);

            obj.transform.position = newPos;
            obj.transform.localScale = new Vector3(1, 1, 4);

            obj.GetComponent<Renderer>().material.color = (tilePos.xIndex + tilePos.yIndex) % 2 == 0 ? Color.black : Color.white;
        }
    }
}