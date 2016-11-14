namespace CFE
{
    using System;
    using UnityEngine;

    class AbilityIndicatorControl : MonoBehaviour
    {
        Mesh mesh;
        [SerializeField, Range(0,10)]
        int _radius;
        int radius;

        AbilityAOE_Type aoeType;

        #region MonoBehaviours
        void Start()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            aoeType = AbilityAOE_Type.SingleTile;

            //setRadialIndicator(_radius);
            //setLinearIndicator(new TilePosition(0, 0), new TilePosition(9, -2));
        }
        
        void OnEnable()
        {
            InputManager.FrameInputEvent += OnFrameInputEvent;
        }

        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnFrameInputEvent;
        }

        void Update()
        {

        }
        #endregion

        public void setIndicator(AbilityBehaviorData data)
        {
            aoeType = data.AoEType;
            if (data.AoEType == AbilityAOE_Type.Radial)
            {
                radius = data.radius;
                _radius = radius;
                setRadialIndicator(radius);
                Debug.Log("Setting radial indicator");
                return;
            }
            else if(aoeType == AbilityAOE_Type.Linear)
            {
                Debug.Log("Setting linear indicator");
            }
            setParent();
        }

        #region Event Responders
        private void OnFrameInputEvent(FrameInputData data)
        {
            //if (_followMouse)
            //    transform.position = data.tilePos.tilePosition;
            //setLinearIndicator(data.activeEntityTilePos, data.tilePos);
            if(aoeType == AbilityAOE_Type.Radial)
            {
                transform.parent.position = data.tilePos.tilePosition;
            }
            else if (aoeType == AbilityAOE_Type.Linear)
            {

                setLinearIndicator(data.activeEntityTilePos, data.tilePos);
            }
        }

        #endregion


        private void setParent()
        {
            if (aoeType == AbilityAOE_Type.Radial)
            {
                transform.parent.parent = null;
                transform.parent.position = new Vector3(0, 0, -0.1f);
                transform.eulerAngles = Vector3.zero;
            }
            else if (aoeType == AbilityAOE_Type.Linear)
            {
                transform.parent.parent = EntityManager.activePlayer.transform;
                transform.parent.localPosition = Vector3.zero;
                transform.parent.Translate(new Vector3(0, 0, -0.1f));
            }
        }

        private void setRadialIndicator(int radius)
        {
            mesh.Clear();
            int tileCount = getRadialTileCount(radius);

            Vector3[] vertices = new Vector3[tileCount * 6];
            int[] triangles = new int[tileCount * 6];

            int triangleIndex = 0;
            int vertexIndex = 0;

            addRadialTiles(vertices, triangles, ref vertexIndex, ref triangleIndex, radius);
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        private void setLinearIndicator(TilePosition startPos, TilePosition endPos)
        {
            mesh.Clear();
            TilePosition relativeEndPos = endPos - startPos;

            int tileCount = Mathf.Abs(relativeEndPos.xIndex) > Mathf.Abs(relativeEndPos.yIndex) ? relativeEndPos.xIndex : relativeEndPos.yIndex;
            tileCount = Mathf.Abs(tileCount);
            tileCount++;

            Vector3[] vertices = new Vector3[tileCount * 6];
            int[] triangles = new int[tileCount * 6];

            int triangleIndex = 0;
            int vertexIndex = 0;

            addLinearTiles(vertices, triangles, ref vertexIndex, ref triangleIndex, startPos, endPos);
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        private void addRadialTiles(Vector3[] array, int[] triangles, ref int index, ref int triangleIndex, int radius)
        {
            AddTile(array, triangles, ref index, ref triangleIndex, 0, 0);
            if (radius == 0)
                return;

            for (int i = 1; i < 1 + radius; i++)
            {
                AddTile(array, triangles, ref index, ref triangleIndex, 0, i);
                AddTile(array, triangles, ref index, ref triangleIndex, 0, -i);
                AddTile(array, triangles, ref index, ref triangleIndex, i, 0);
                AddTile(array, triangles, ref index, ref triangleIndex, -i, 0);
            }
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= radius - i; j++)
                {
                    AddTile(array, triangles, ref index, ref triangleIndex, i, j);
                    AddTile(array, triangles, ref index, ref triangleIndex, -i, j);
                    AddTile(array, triangles, ref index, ref triangleIndex, i, -j);
                    AddTile(array, triangles, ref index, ref triangleIndex, -i, -j);
                }
            }
        }

        private void addLinearTiles(Vector3[] array, int[] triangles, ref int index, ref int triangleIndex, TilePosition startPos, TilePosition endPos)
        {
            TilePosition relativeEnd = endPos - startPos;
            int xEnd = Mathf.Abs(relativeEnd.xIndex);
            int yEnd = Mathf.Abs(relativeEnd.yIndex);

            int slope = xEnd == 0 ? 0 : yEnd / xEnd;

            xEnd++;
            yEnd++;

            if(xEnd > yEnd)
            {
                for (int i = 0; i < xEnd; i++)
                {
                    if(yEnd ==0)
                        AddTile(array, triangles, ref index, ref triangleIndex, i, 0);
                    else
                        AddTile(array, triangles, ref index, ref triangleIndex, i, (i * yEnd / xEnd));
                }
            }
            else
            {
                for (int i = 0; i < yEnd; i++)
                {
                    AddTile(array, triangles, ref index, ref triangleIndex, (i * xEnd / yEnd), i);
                }
            }
            adjustLinearRotation(relativeEnd);
        }

        private void adjustLinearRotation(TilePosition relativeEnd)
        {
            if (relativeEnd.xIndex < 0)
            {
                if (relativeEnd.yIndex < 0)
                    transform.localEulerAngles = new Vector3(180, 180, 0);
                else
                    transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                if (relativeEnd.yIndex < 0)
                    transform.localEulerAngles = new Vector3(180, 0, 0);
                else
                    transform.localEulerAngles = Vector3.zero;
            }
        }

        private void AddTile(Vector3[] array, int[] triangles, ref int arrayIndex, ref int triangleIndex, int xPos, int yPos)
        {
            array[0 + arrayIndex] = Grid.getTileCorner(xPos, yPos, 0);
            array[1 + arrayIndex] = Grid.getTileCorner(xPos, yPos, 1);
            array[2 + arrayIndex] = Grid.getTileCorner(xPos, yPos, 1);
            array[3 + arrayIndex] = Grid.getTileCorner(xPos, yPos, 2);
            array[4 + arrayIndex] = Grid.getTileCorner(xPos, yPos, 3);
            array[5 + arrayIndex] = Grid.getTileCorner(xPos, yPos, 3);

            triangles[0 + triangleIndex] = 5 + arrayIndex;
            triangles[1 + triangleIndex] = 0 + arrayIndex;
            triangles[2 + triangleIndex] = 1 + arrayIndex;
            triangles[3 + triangleIndex] = 2 + arrayIndex;
            triangles[4 + triangleIndex] = 3 + arrayIndex;
            triangles[5 + triangleIndex] = 4 + arrayIndex;

            triangleIndex += 6;
            arrayIndex += 6;
        }

        /**
        *<summary>
        *Returns the number of tiles in a Radial arrangement for a given radius
        *</summary>
        */
        private int getRadialTileCount(int radius)
        {
            if(radius < 0)
            {
                Debug.LogError("An incorrect value was passed");
                return -1;
            }
            else if(radius == 0)
            {
                return 1;
            }
            else
            {
                return 4 * radius + getRadialTileCount(radius - 1);
            }
        }

        private void getLinearSlope(out int deltaX, out int deltaY, TilePosition startPos, TilePosition endPos)
        {
            int xMod, yMod;

            if (startPos.xIndex == endPos.xIndex)
                xMod = 0;
            else if (startPos.xIndex > endPos.xIndex)
                xMod = -1;
            else
                xMod = 1;

            if (startPos.yIndex == endPos.yIndex)
                yMod = 0;
            else if (startPos.yIndex > endPos.yIndex)
                yMod = -1;
            else
                yMod = 1;

            deltaX = endPos.xIndex - startPos.xIndex + xMod;
            deltaY = endPos.yIndex - startPos.yIndex + yMod;
        }
    }
}