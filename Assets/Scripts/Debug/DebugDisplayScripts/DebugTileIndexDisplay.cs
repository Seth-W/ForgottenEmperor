namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class DebugTileIndexDisplay : MonoBehaviour
    {
        TextMesh text;

        void Start()
        {
            text = GetComponent<TextMesh>();
            text.text = new TilePosition(transform.position).ToString();
            transform.Translate(new Vector3(0, 0, -0.2f));
        }

    }
}