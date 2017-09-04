namespace CFE
{
    using UnityEngine;

    class DataManager : MonoBehaviour
    {
        [SerializeField]
        int _width, _height;
        private static int _Width, _Height;
        public static int Width { get { return _Width; } }
        public static int Height { get { return _Height; } }
        private static bool alreadyInitialized = false;

        void Start()
        {
            if (alreadyInitialized)
            {
                Debug.LogError("There is more than one DataManager in this scene");
                Destroy(this);
            }
            alreadyInitialized = true;
            _Width = _width;
            _Height = _height;
            Pathfinder.init();
        }

    }
}
