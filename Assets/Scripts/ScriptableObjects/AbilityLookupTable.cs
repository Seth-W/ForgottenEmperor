namespace CFE
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Ability Lookup Table", menuName = "ScriptableObjects/Lookup Tables", order = 7)]
    class AbilityLookupTable : ScriptableObject
    {
        public AbilityBehaviorData[] _abilityLookupTable;
    }
}
