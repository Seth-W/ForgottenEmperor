namespace CFE
{
    using UnityEngine;

    class Grid
    {
        /**
        *<summary>
        *Returns the <see cref="Vector3"/> for a the given corner of a tile at a <see cref="TilePosition"/> 
        *</summary>
        */
        public static Vector3 getTileCorner(TilePosition tilePos, int corner)
        {
            return getTileCorner(tilePos.xIndex, tilePos.yIndex, corner);
        }

        /**
        *<summary>
        *Returns the <see cref="Vector3"/> for a the given corner of a tile at a <see cref="TilePosition"/> 
        *</summary>
        */
        public static Vector3 getTileCorner(int xIndex, int yIndex, int corner)
        {
            return getTileCorner(xIndex, yIndex, corner, 0.5f);
        }

        /**
        *<summary>
        *Returns the <see cref="Vector3"/> for a the given corner of a tile at a <see cref="TilePosition"/>  offset by a certain float
        *</summary>
        */
        public static Vector3 getTileCorner(int xIndex, int yIndex, int corner, float borderWidth)
        {
            Vector3 retValue = new Vector3();
            retValue += Vector3.up * yIndex;
            retValue += Vector3.right * xIndex;
            if (corner == 0)
            {
                retValue.x -= borderWidth;
                retValue.y += borderWidth;
            }
            else if (corner == 1)
            {
                retValue.x += borderWidth;
                retValue.y += borderWidth;
            }
            else if (corner == 2)
            {
                retValue.x += borderWidth;
                retValue.y -= borderWidth;
            }
            else if (corner == 3)
            {
                retValue.x -= borderWidth;
                retValue.y -= borderWidth;
            }
            else
            {
                Debug.LogError("Int Corner not a valid value");
                return new Vector3();
            }
            return retValue;
        }
    }
}
