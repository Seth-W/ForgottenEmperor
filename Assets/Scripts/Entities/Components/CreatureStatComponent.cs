namespace CFE
{
    using UnityEngine;

    class CreatureStatComponent : MonoBehaviour, EStat
    {
        [SerializeField]
        int str, agi, intel, armor;

        StatBlock statBlock;

        void Start()
        {
            statBlock = new StatBlock(str, agi, intel, armor);
        }

        public StatBlock getStats()
        {
            return statBlock;
        }

        public void setStats(StatBlock stats)
        {
            statBlock = stats;
        }
    }
}