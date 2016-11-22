namespace CFE
{
    using UnityEngine;

    [System.Serializable]
    struct AbilityBehaviorData
    {
        public string abilityName;
        public AbilityAOE_Type AoEType;
        public AbilityTargetingType targetType;
        public GameObject projectilePrefab, tileEffectPrefab;
        public int radius, length, range, width, manaCost;
    }
}
