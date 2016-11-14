namespace CFE.Extensions
{
    using UnityEngine;

    class Vector2Helper
    {

        public static Vector2[] getLine(Vector2 startPos, Vector2 endPos)
        {
            Vector2 relativeEnd = endPos - startPos;
            int xEnd = (int)Mathf.Abs(relativeEnd.x);
            int yEnd = (int)Mathf.Abs(relativeEnd.y);

            int slope = xEnd == 0 ? 0 : yEnd / xEnd;

            xEnd++;
            yEnd++;

            Vector2[] retValue = new Vector2[xEnd > yEnd ? xEnd : yEnd];

            if (xEnd > yEnd)
            {
                for (int i = 0; i < xEnd; i++)
                {
                    if (yEnd == 0)
                        retValue[i] = new Vector2(i, 0);
                    else
                        retValue[i] = new Vector2(i, i * yEnd / xEnd);
                }
            }
            else
            {
                for (int i = 0; i < yEnd; i++)
                {
                    retValue[i] = new Vector2(i * xEnd / yEnd, i);
                }
            }

            return applyLinearRotation(retValue, startPos, endPos);
            //return retValue;
        }

        public static Vector2[] getRadial(int radius)
        {
            Vector2[] retValue = new Vector2[getRadialTileCount(radius)];

            if (retValue.Length == 0)
                return null;

            retValue[0] = Vector2.zero;

            if (radius == 0)
                return retValue;

            int index = 1;
            for (int i = 1; i < 1 + radius; i++)
            {
                retValue[++index] = new Vector2(0, i);
                retValue[++index] = new Vector2(0, -i);
                retValue[++index] = new Vector2(i, 0);
                retValue[++index] = new Vector2(-i, 0);
            }
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= radius - i; j++)
                {
                    retValue[++index] = new Vector2(i, j);
                    retValue[++index] = new Vector2(-i, j);
                    retValue[++index] = new Vector2(i, -j);
                    retValue[++index] = new Vector2(-i, -j);
                }
            }
            return retValue;
        }


        private static Vector2[] applyLinearRotation(Vector2[] array, Vector2 startPos, Vector2 endPos)
        {
            Vector2[] retValue = new Vector2[array.Length];
            array.CopyTo(retValue, 0);


            if(startPos.x > endPos.x)
            {
                for (int i = 0; i < retValue.Length; i++)
                {
                    retValue[i].x *= -1;
                }
            }
            if(startPos.y > endPos.y)
            {
                for (int i = 0; i < retValue.Length; i++)
                {
                    retValue[i].y *= -1;
                }
            }

            return retValue;
        }

        private static int getRadialTileCount(int radius)
        {
            if (radius < 0)
            {
                Debug.LogError("An incorrect value was passed");
                return -1;
            }
            else if (radius == 0)
            {
                return 1;
            }
            else
            {
                return 4 * radius + getRadialTileCount(radius - 1);
            }
        }
    }
}
