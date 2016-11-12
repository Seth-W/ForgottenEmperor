namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    class EntityManager : MonoBehaviour
    {
        public delegate void ActiveEntityUpdate(EntityModel activeEntity);
        public static ActiveEntityUpdate ActiveEntityUpdateEvent;

        int _activePlayerIndex = 0;
        int _activeAbilityIndex = 0;
        List<EntityModel> _playerEntities;
        static List<EntityModel> playerEntities;
        static int activePlayerIndex = 0;
        static int activeAbilityIndex = 0;
        public static int currentAbilityIndex { get { return activePlayerIndex * 4 + activeAbilityIndex; } }

        public static EntityModel activePlayer { get { return playerEntities[activePlayerIndex]; } }

        [SerializeField]
        Transform pathfindingDebugObj;

        #region MonoBehaviours
        void OnEnable()
        {
            EntityModel.EntitySpawnEvent += OnEntitySpawn;
            CharacterSelectButton.CharacterSelectButtonClickEvent += OnCharacterSelectButtonClick;
            AbilitySelectButton.AbilitySelectButtonClickEvent += OnAbilitySelectButtonClick;
            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnDisable()
        {
            EntityModel.EntitySpawnEvent -= OnEntitySpawn;
            CharacterSelectButton.CharacterSelectButtonClickEvent -= OnCharacterSelectButtonClick;
            AbilitySelectButton.AbilitySelectButtonClickEvent -= OnAbilitySelectButtonClick;
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
                    UpdateActivePlayerIndex(0);
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
            if(model.type == EntityType.Player)
                _playerEntities.Add(model);
        }

        /**
        *<summary>
        *Updates the index of the active entity
        *</summary>
        */
        private void UpdateActivePlayerIndex(int playerIndex)
        {
            _activePlayerIndex = playerIndex;
            activePlayerIndex = playerIndex;
            pathfindingDebugObj.parent = _playerEntities[_activePlayerIndex].transform;
            pathfindingDebugObj.localPosition = Vector3.zero;
            ActiveEntityUpdateEvent(activePlayer);
        }

        private void OnCharacterSelectButtonClick(int playerIndex)
        {
            UpdateActivePlayerIndex(playerIndex);
        }

        private void OnAbilitySelectButtonClick(int character, int button)
        {
            UpdateActivePlayerIndex(character);
            _activeAbilityIndex = button;
            activeAbilityIndex = button;
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