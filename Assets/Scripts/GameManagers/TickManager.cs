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
        int ticksPerSecond;

        static int _TicksPerSecond;
        public static int TicksPerSecond { get { return _TicksPerSecond; } }

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
            _TicksPerSecond = ticksPerSecond;
        }

        private Tick getTickData()
        {
            return new Tick();
        }
    }
}