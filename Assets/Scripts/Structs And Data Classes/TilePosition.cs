namespace CFE
{
    using UnityEngine;

    [System.Serializable]
    public struct TilePosition
    {
        public static TilePosition nullTPos = new TilePosition(int.MinValue, int.MinValue);

        public Vector2 tilePosition;
        public int xIndex, yIndex;

        public TilePosition(Vector3 worldPosition)
        {
            xIndex = Mathf.FloorToInt(worldPosition.x + 0.5f);
            yIndex = Mathf.FloorToInt(worldPosition.y + 0.5f);

            xIndex = Mathf.Clamp(xIndex, -DataManager.Width / 2, DataManager.Width / 2);
            yIndex = Mathf.Clamp(yIndex, -DataManager.Height / 2, DataManager.Height / 2);

            tilePosition = new Vector3();

            tilePosition.x = xIndex;
            tilePosition.y = yIndex;

            xIndex += DataManager.Width / 2;
            yIndex += DataManager.Height / 2;
        }

        public TilePosition(int xIndex, int yIndex)
        {
            this.xIndex = xIndex;
            this.yIndex = yIndex;

            tilePosition = new Vector3();

            tilePosition.x = xIndex - DataManager.Width / 2;
            tilePosition.y = yIndex - DataManager.Height / 2;
        }

        public override string ToString()
        {
            return xIndex + "," + yIndex;
        }


        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            TilePosition pos = (TilePosition)obj;
            return xIndex == pos.xIndex && yIndex == pos.yIndex;
        }

        public override int GetHashCode()
        {
            return xIndex * yIndex;
        }

        public static bool operator == (TilePosition posA, TilePosition posB)
        {
            return (posA.Equals(posB));
        }
        public static bool operator != (TilePosition posA, TilePosition posB)
        {
            return !(posA.Equals(posB));
        }

        public static TilePosition operator - (TilePosition posA, TilePosition posB)
        {
            return new TilePosition(posA.xIndex - posB.xIndex, posA.yIndex - posB.yIndex);
        }

        public static TilePosition operator + (TilePosition posA, TilePosition posB)
        {
            return new TilePosition(posA.xIndex + posB.xIndex, posA.yIndex + posB.yIndex);
        }
    }
}