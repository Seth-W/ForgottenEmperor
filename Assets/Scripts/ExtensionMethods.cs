namespace CFE.Extensions
{
    using UnityEngine;

    public static class ExtensionMethods
    {
        /**
        *<summary>
        *Unzips an array of <see cref="Vector3"/> from an array of <see cref="TilePosition"/>
        *</summary>
        */
        public static Vector3[] ToVector3Array(this TilePosition[] array)
        {
            Vector3[] retValue = new Vector3[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                retValue[i] = array[i].tilePosition;
            }

            return retValue;
        }

        /**
        *<summary>
        *Converts an array of <see cref="Vector2"/> into an array of <see cref="TilePosition"/>
        *</summary>
        */
        public static TilePosition[] convertToTilePosition(this Vector2[] positions)
        {
            TilePosition[] retValue = new TilePosition[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                retValue[i] = new TilePosition (positions[i]);
            }

            return retValue;
        }


    }
}
