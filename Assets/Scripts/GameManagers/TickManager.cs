namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class TickManager : MonoBehaviour
    {
        public delegate void TickUpdate(Tick tickData);
        public static TickUpdate TickUpdateEvent;

        [SerializeField]
        bool tickAutomatically;
        [SerializeField]
        float ticksPerSecond;

        float timeSinceLastTick;

        void Start()
        {
            timeSinceLastTick = Time.time;
        }

        void Update()
        {
            if (!InputManager.isPaused)
            {
                if ((Time.time - timeSinceLastTick) * ticksPerSecond >= 1)
                {
                    TickUpdateEvent(getTickData());
                    timeSinceLastTick = Time.time;
                }
            }
        }

        private Tick getTickData()
        {
            return new Tick();
        }
    }
}