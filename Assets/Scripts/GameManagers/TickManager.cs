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

        float timeAtLastTick;

        void Start()
        {
            timeAtLastTick = Time.time;
            _TicksPerSecond = ticksPerSecond;
        }

        void Update()
        {
            if (!InputManager.isPaused)
            {
                if ((Time.time - timeAtLastTick) * ticksPerSecond >= 1)
                {
                    TickUpdateEvent(getTickData());
                    timeAtLastTick = Time.time;
                }
            }
        }

        private Tick getTickData()
        {
            return new Tick();
        }
    }
}