namespace CFE.MethodExtensions
{
    public static class ExtensionMethods
    {

        /**
        *<summary>
        *Unzips an array of <see cref="UnityEngine.Vector3"/> from an array of <see cref="TilePosition"/>
        *</summary>
        */
        public static UnityEngine.Vector3[] ToVector3Array(this TilePosition[] array)
        {
            UnityEngine.Vector3[] retValue = new UnityEngine.Vector3[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                retValue[i] = array[i].tilePosition;
            }

            return retValue;
        }
    }
}
