namespace CFE
{
    using System;
    using UnityEngine;

    class DebugTileEntityDisplay : MonoBehaviour, IDebugDisplay
    {
        TextMesh text;
        Renderer rend;

        TilePosition tilePos;

        void Start()
        {
            text = GetComponent<TextMesh>();
            rend = GetComponent<Renderer>();
            tilePos = new TilePosition(transform.position);
            transform.Translate(new Vector3(0, 0, -0.2f));
        }

        void Update()
        {
            if (TileManager.getTile(tilePos).Model == null)
            {
                text.text = "";
            }
            else
            {
                text.text = TileManager.getTile(tilePos).Model.gameObject.name;
            }
        }

        public void Display(bool b)
        {
            rend.enabled = b;
        }
    }
}
