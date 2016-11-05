namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class EntityView : MonoBehaviour
    {
        [SerializeField]
        Color color;

        void Start()
        {
            GetComponent<Renderer>().material.color = color;
        }
    }
}