namespace CFE
{
    using UnityEngine;
    using Extensions;
    using Actions.EntityActions;
    using System;

    class EntityControl : MonoBehaviour
    {
        protected EntityModel model;

        /// <summary>
        /// The end position for all the Queued Move Actions
        /// </summary>
        TilePosition currentFullPathEndPos;


        void Start()
        {
        }

        void OnEnable()
        {
            model = GetComponent<EntityModel>();
        }

        void OnDisable()
        {
        }

        public void OnFrameInput(FrameInputData data)
        {
            if (data.mouseData.mouse1Down && data.inPlayableArea)
            {
                Move(data.tilePos, Input.GetKey(KeyCode.LeftShift));
            }
            
            else if(data.mouseData.mouse0Down && data.inPlayableArea)
            {
                if (abilityTargetMatch(data.tilePos, AbilityManager.activeAbility.targetType))
                    castAbility(data.tilePos, AbilityManager.activeAbility, Input.GetKey(KeyCode.LeftShift));
            }
        }

        /**
        *<summary>
        *Checks the ability targeting type against what is on the tile. Returns true if the ability can be cast
        *</summary>
        */
        private bool abilityTargetMatch(TilePosition target, AbilityTargetingType abilityType)
        {
            if(abilityType == AbilityTargetingType.Ground)
                return true;
            EntityModel entity = TileManager.getTile(target).Model;
            if (entity != null)
            {
                Debug.Log("Checkign against entity on tile");
                if (abilityType == AbilityTargetingType.Allies)
                    return entity.type == EntityType.Player;
                else if (abilityType == AbilityTargetingType.Enemies)
                    return entity.type == EntityType.Enemy;
                else if (abilityType == AbilityTargetingType.Both)
                    return true;
            }
            return false;  
        }

        private void castAbility(TilePosition target, AbilityBehaviorData abilityData, bool queueAction)
        {
            if (queueAction)
            {
                model.enqueueAction(new MoveAction(transform, model, target, abilityData.range));
                model.enqueueAction(new DebugMessageAction("Casting an action"));
            }
            else
            {
                model.runAction(new MoveAction(transform, model, target, abilityData.range));
                model.enqueueAction(new DebugMessageAction("Casting an action"));
            }
        }

        private void castFriendlyAction(bool queueAction)
        {
            if (queueAction)
                model.enqueueAction(new DebugMessageAction("Casting a friendly action"));
            else
                model.runAction(new DebugMessageAction("Casting a friendly action"));
        }

        /**
        *<summary>
        *Queues a <see cref="MoveAction"/> for this tile
        *<para>If <paramref name="queueAction"/> is false, simply runs the action instead</para>
        *</summary>
        */
        private void Move(TilePosition destination, bool queueAction)
        {
            if (queueAction)
                model.enqueueAction(new MoveAction(transform, model, destination));
            else
                model.runAction(new MoveAction(transform, model, destination));
        }

        public void setCurrentFullPathEndPos(TilePosition endPos)
        {
            currentFullPathEndPos = endPos;
        }
    }
}