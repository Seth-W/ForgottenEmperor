namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;

    class EntityManager : MonoBehaviour
    {
        int activePlayerIndex;
        List<EntityModel> playerEntities;

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
            activePlayerIndex = 0;

            if(playerEntities != null)
            {
                if (playerEntities.Count > 0)
                {
                    pathfindingDebugObj.parent = playerEntities[activePlayerIndex].transform;
                    pathfindingDebugObj.localPosition = Vector3.zero;
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
        void OnEntitySpawn(EntityModel model)
        {
            if (playerEntities == null)
                playerEntities = new List<EntityModel>();
            playerEntities.Add(model);
        }

        /**
        *<summary>
        *Updates the index of the active entity
        *</summary>
        */
        void OnCharacterSelectButtonClick(int playerIndex)
        {
            activePlayerIndex = playerIndex;
            pathfindingDebugObj.parent = playerEntities[activePlayerIndex].transform;
            pathfindingDebugObj.localPosition = Vector3.zero;
        }

        /**
        *<summary>
        *Handles input data and distributes it to the active entity
        *</summary>
        */
        void OnFrameInput(FrameInputData data)
        {
            playerEntities[activePlayerIndex].control.OnFrameInput(data);
        }
        #endregion
    }
}