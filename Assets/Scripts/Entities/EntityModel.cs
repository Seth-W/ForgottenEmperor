namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;
    using CFE.Actions.EntityActions;

    class EntityModel : MonoBehaviour
    {
        public delegate void EntitySpawn(EntityModel model);
        public static EntitySpawn EntitySpawnEvent;

        [SerializeField]
        float _speed;
        public float speed { get { return _speed; } }


        [SerializeField]
        float _strength;
        public float strength { get { return _strength; } }

        [SerializeField]
        EntityType _type;
        public EntityType type { get { return _type; } }

        public TilePosition tilePos { get { return new TilePosition(transform.position); } }

        EntityControl _control;
        public EntityControl control { get { return _control; } }
        EntityView _view;
        public EntityView view { get { return _view; } }

        Animator _anim;

        public bool isMoving
        {
            get
            {
                if (currentAction != null)
                    return currentAction.GetType() == typeof(MoveAction);
                else
                    return false;
            }
        }
        bool cancellingAction;
        public bool queuedActions { get { return actionQueue.Count > 0; } }

        Queue<IAction> actionQueue;
        public IAction currentAction;

        bool _alive;
        public bool alive { get { return _alive; } }
        
        #region MonoBehaviours
        void Start()
        {
            actionQueue = new Queue<IAction>();

            _control = GetComponent<EntityControl>();
            _view = GetComponent<EntityView>();
            _alive = true;

            _anim = GetComponentInChildren<Animator>();

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
            if (!alive)
                return;
            if(currentAction != null)
            {
                if (cancellingAction)
                {
                    cancellingAction = !currentAction.cancel();
                    if (!cancellingAction)
                        currentAction = actionQueue.Dequeue();
                }
                else
                {
                    bool dequeue = currentAction.execute();
                    if (dequeue && actionQueue.Count > 0)
                        currentAction = actionQueue.Dequeue();
                    else if (dequeue)
                        currentAction = null;
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
        *Forces the given IAction to the front of the queue
        *</summary>
        */
        public void forceEnqueueAction(IAction action)
        {
            Queue<IAction> temp = new Queue<IAction>();
            temp.Enqueue(currentAction);
            currentAction = action;
            while(actionQueue.Count > 0)
            {
                temp.Enqueue(actionQueue.Dequeue());
            }
            actionQueue = temp;
        }

        /**
        *<summary>
        *Sets the given <see cref="IAction"/> to be the current action for the entity and clears any queued actions
        *</summary>
        */
        public void runAction(IAction action)
        {
            actionQueue.Clear();
            actionQueue.Enqueue(action);
            //currentAction = action;
            cancellingAction = currentAction != null;
        }

        /**
        *<summary>
        *Sets the alive property for this Entity
        *</summary>
        */
        public void setAlive(bool b)
        {
            _alive = b;
            if (!b)
                Debug.Log(" \'killing\' this entity");
        }

        /**
        *<summary>
        *Sets an integer animator parameter for this entity
        *</summary>
        */
        public void setAnimatorParams(string s, int i)
        {
            _anim.SetInteger(s, i);
        }

        /**
        *<summary>
        *Sets a float animator parameter for this entity
        *</summary>
        */
        public void setAnimatorParams(string s, float f)
        {
            _anim.SetFloat(s, f);
        }

        /**
        *<summary>
        *Sets a boolean animator parameter for this entity
        *</summary>
        */
        public void setAnimatorParams(string s, bool b)
        {
            _anim.SetBool(s, b);
        }

        /**
        *<summary>
        *Activates a trigger animation parameter for this entity
        *</summary>
        */
        public void setAnimatorParams(string s)
        {
            _anim.SetTrigger(s);
        }
    }
}