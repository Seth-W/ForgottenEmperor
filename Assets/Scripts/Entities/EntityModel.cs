namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class EntityModel : MonoBehaviour
    {
        [SerializeField]
        float _speed;
        public float speed { get { return _speed; } }

        TilePosition _tilePos;
        TilePosition tilePos { get { return _tilePos; } }

        EntityControl _control;
        public EntityControl control { get { return _control; } }
        EntityView _view;
        public EntityView view { get { return _view; } }

        void Start()
        {
            _control = GetComponent<EntityControl>();
            _view = GetComponent<EntityView>();
        }
    }
}