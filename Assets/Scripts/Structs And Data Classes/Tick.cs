namespace CFE
{
    using UnityEngine;

    class Tick
    {
        public static float Time, DeltaTime;

        public Tick()
        {
            Time += UnityEngine.Time.deltaTime;
            DeltaTime = UnityEngine.Time.deltaTime;
        }
    }
}
