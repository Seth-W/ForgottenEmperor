namespace CFE
{
    using UnityEngine;

    class DataManager : MonoBehaviour
    {
        [SerializeField]
        int _width, _height;
        public static int Width, Height;
        private static bool alreadyInitialized = false;

        void Start()
        {
            if (alreadyInitialized)
            {
                Debug.LogError("There is more than one DataManager in this scene");
                Destroy(this);
            }
            alreadyInitialized = true;
            Width = _width;
            Height = _height;
            Pathfinder.init();
        }

    }
}
