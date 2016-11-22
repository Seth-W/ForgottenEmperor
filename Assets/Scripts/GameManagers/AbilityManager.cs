namespace CFE
{
    using UnityEngine;
    using Actions.EntityActions;
    using Extensions;
    using System.Collections.Generic;

    class AbilityManager : MonoBehaviour
    {
        public static AbilityBehaviorData activeAbility;
        static Dictionary<string, AbilityBehaviorData> abilityBehavior;

        [SerializeField]
        AbilityIndicatorControl spellIndicator;

        [SerializeField]
        AbilityLookupTable abilityLookupTable;

        Dictionary<string, AbilityBehaviorData> _abilityBehavior;


        #region
        void Start()
        {
            _abilityBehavior = new Dictionary<string, AbilityBehaviorData>();
            abilityBehavior = _abilityBehavior;

            for (int i = 0; i < abilityLookupTable._abilityLookupTable.Length; i++)
            {
                _abilityBehavior.Add(abilityLookupTable._abilityLookupTable[i].abilityName, abilityLookupTable._abilityLookupTable[i]);
            }
        }

        void OnEnable()
        {
            AbilitySelectButton.AbilitySelectButtonClickEvent += OnAbilitySelectButtonClickEvent;
            AbilityAction.AbilityCastEvent += OnAbilityCastEvent;
        }

        void OnDisable()
        {
            AbilitySelectButton.AbilitySelectButtonClickEvent -= OnAbilitySelectButtonClickEvent;
            AbilityAction.AbilityCastEvent -= OnAbilityCastEvent;
        }
        #endregion

        public static AbilityBehaviorData getAbilityData(string key)
        {
            return abilityBehavior[key];
        }

        private void OnAbilitySelectButtonClickEvent(int characterIndex, string abilityKey)
        {
            spellIndicator.enabled = true;
            activeAbility = _abilityBehavior[abilityKey];
            spellIndicator.setIndicator(activeAbility);
        }

        private void OnAbilityCastEvent(TilePosition target, AbilityBehaviorData abilityData, EntityModel castingEntity)
        {
            //if (abilityData.AoEType == AbilityAOE_Type.Radial)
            //    CastRadialAbility(target, abilityData, castingEntity);
            GameObject obj = Instantiate(abilityData.projectilePrefab, castingEntity.tilePos.tilePosition, Quaternion.identity) as GameObject;
            obj.GetComponent<IProjectile>().Initiate(castingEntity.tilePos, target, castingEntity.GetComponent<EStat>().getStats());
        }

        private void CastRadialAbility(TilePosition target, AbilityBehaviorData abilityData, EntityModel castingEntity)
        {
            Vector2[] tilePositions = Vector2Helper.getRadial(abilityData.radius);

            for (int i = 0; i < tilePositions.Length; i++)
            {
                tilePositions[i] += target.tilePosition;
                GameObject obj = Instantiate(abilityData.tileEffectPrefab, tilePositions[i], Quaternion.identity) as GameObject;
                obj.GetComponent<ITileEffect>().Initialize(castingEntity.GetComponent<EStat>().getStats());
            }

            for (int i = 0; i < Vector2Helper.getRadialTileCount(abilityData.radius); i++)
            {
            }
        }
    }
}
