﻿namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;

    class EntityModel : MonoBehaviour
    {
        public delegate void EntitySpawn(EntityModel model);

        public static EntitySpawn EntitySpawnEvent;

        [SerializeField]
        float _speed;
        public float speed { get { return _speed; } }

        TilePosition _tilePos;
        TilePosition tilePos { get { return _tilePos; } }

        EntityControl _control;
        public EntityControl control { get { return _control; } }
        EntityView _view;
        public EntityView view { get { return _view; } }

        bool _isMoving;
        public bool isMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }
        public bool queuedActions { get { return actionQueue.Count > 0; } }

        Queue<IAction> actionQueue;
        IAction currentAction;
        
        #region MonoBehaviours
        void Start()
        {
            actionQueue = new Queue<IAction>();

            _control = GetComponent<EntityControl>();
            _view = GetComponent<EntityView>();

            EntitySpawnEvent(this);

            TileManager.getTile(new TilePosition(transform.position)).updateEntity(this);
        }

        void OnEnable()
        {
            TickManager.TickUpdateEvent += OnTickUpdate;
        }

        void OnDisable()
        {
            TickManager.TickUpdateEvent -= OnTickUpdate;
        }
        #endregion

        /**
        *<summary>
        *Executes the current Action. If the current action finishes executing dequeues the next action
        *</summary>
        */
        void OnTickUpdate(Tick data)
        {
            if(currentAction != null)
            {
                while(currentAction.execute() && actionQueue.Count > 0)
                {
                    currentAction = actionQueue.Dequeue();
                }
            }
            else
            {
                if (actionQueue.Count > 0)
                    currentAction = actionQueue.Dequeue();
            }
        }

        /**
        *<summary>
        *Adds the given <see cref="IAction"/> to the action queue for the entity
        *</summary>
        */
        public void enqueueAction(IAction action)
        {
            actionQueue.Enqueue(action);
        }

        /**
        *<summary>
        *Sets the given <see cref="IAction"/> to be the current action for the entity and clears any queued actions
        *</summary>
        */
        public void runAction(IAction action)
        {
            actionQueue.Clear();
            currentAction = action;
        }
    }
}