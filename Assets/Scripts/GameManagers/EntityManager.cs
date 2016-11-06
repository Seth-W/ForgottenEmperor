namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;

    class EntityManager : MonoBehaviour
    {
        int _activePlayerIndex = 0;
        List<EntityModel> _playerEntities;
        static List<EntityModel> playerEntities;
        static int activePlayerIndex = 0; 

        public static EntityModel activePlayer { get { return playerEntities[activePlayerIndex]; } }

        [SerializeField]
        Transform pathfindingDebugObj;

        #region MonoBehaviours
        void OnEnable()
        {
            EntityModel.EntitySpawnEvent += OnEntitySpawn;
            CharacterSelectButton.CharacterSelectButtonClickEvent += OnCharacterSelectButtonClick;
            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnDisable()
        {
            EntityModel.EntitySpawnEvent -= OnEntitySpawn;
            CharacterSelectButton.CharacterSelectButtonClickEvent -= OnCharacterSelectButtonClick;
            InputManager.FrameInputEvent -= OnFrameInput;
        }

        void Start()
        {
            _playerEntities = new List<EntityModel>();
            playerEntities = _playerEntities;

            if (_playerEntities != null)
            {
                if (_playerEntities.Count > 0)
                {
                    UpdateActiveIndex(0);
                }
            }
        }
        #endregion

        #region Event Responders
        /**
        *<summary>
        *Tracks the given Entity Model
        *</summary>
        */
        private void OnEntitySpawn(EntityModel model)
        {
            if (_playerEntities == null)
                _playerEntities = new List<EntityModel>();
            _playerEntities.Add(model);
        }

        /**
        *<summary>
        *Updates the index of the active entity
        *</summary>
        */
        private void UpdateActiveIndex(int playerIndex)
        {
            _activePlayerIndex = playerIndex;
            activePlayerIndex = playerIndex;
            pathfindingDebugObj.parent = _playerEntities[_activePlayerIndex].transform;
            pathfindingDebugObj.localPosition = Vector3.zero;
        }

        private void OnCharacterSelectButtonClick(int playerIndex)
        {
            UpdateActiveIndex(playerIndex);
        }

        /**
        *<summary>
        *Handles input data and distributes it to the active entity
        *</summary>
        */
        private void OnFrameInput(FrameInputData data)
        {
            _playerEntities[_activePlayerIndex].control.OnFrameInput(data);
        }
        #endregion
    }
}