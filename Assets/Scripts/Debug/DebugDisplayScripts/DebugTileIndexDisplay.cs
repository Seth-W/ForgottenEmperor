namespace CFE
{
    using System;
    using UnityEngine;

    class DebugTileIndexDisplay : MonoBehaviour, IDebugDisplay
    {
        TextMesh text;
        MeshRenderer rend;

        void Start()
        {
            text = GetComponent<TextMesh>();
            rend = GetComponent<MeshRenderer>();

            text.text = new TilePosition(transform.position).ToString();
            transform.Translate(new Vector3(0, 0, -0.2f));
        }

        public void Display(bool b)
        {
            rend.enabled = b;
        }
        
    }
}