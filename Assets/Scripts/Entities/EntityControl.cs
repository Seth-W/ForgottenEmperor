namespace CFE
{
    using UnityEngine;
    using MethodExtensions;
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
            if (data.mouseData.mouse0Down && data.inPlayableArea)
            {
                EntityModel model = TileManager.getTile(data.tilePos).Model;
                if (model == null)
                    Move(data.tilePos, Input.GetKey(KeyCode.LeftShift));
                else if (model.type == EntityType.Player)
                    castFriendlyAction(Input.GetKey(KeyCode.LeftShift));
                else if (model.type == EntityType.Enemy)
                    meleeAttack(data.tilePos, Input.GetKey(KeyCode.LeftShift)) ;
            }
        }

        private void castAbility(bool queueAction, TilePosition target)
        {
            if (queueAction)
            {
                model.enqueueAction(new MoveAction(transform, model, target, AbilityManager.activeAbility.range));
                model.enqueueAction(new DebugMessageAction("Casting an enemy action"));
            }
            else
            {
                model.runAction(new MoveAction(transform, model, target, AbilityManager.activeAbility.range));
                model.enqueueAction(new DebugMessageAction("Casting an enemy action"));
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

        private void meleeAttack(TilePosition target, bool queueAction)
        {
            if (queueAction)
            {
                model.enqueueAction(new MoveAction(transform, model, target, 1));
                model.enqueueAction(new DebugMessageAction("Casting an enemy action"));
            }
            else
            {
                model.runAction(new MoveAction(transform, model, target, 1));
                model.enqueueAction(new DebugMessageAction("Casting an enemy action"));
            }
        }

        public void setCurrentFullPathEndPos(TilePosition endPos)
        {
            currentFullPathEndPos = endPos;
        }
    }
}