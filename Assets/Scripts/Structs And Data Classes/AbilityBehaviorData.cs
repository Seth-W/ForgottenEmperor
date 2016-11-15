namespace CFE
{
    using UnityEngine;

    [System.Serializable]
    struct AbilityBehaviorData
    {
        public AbilityAOE_Type AoEType;
        public AbilityTargetingType targetType;
        public GameObject abilityPrefab;
        public int radius, length, range, width;
    }
}
