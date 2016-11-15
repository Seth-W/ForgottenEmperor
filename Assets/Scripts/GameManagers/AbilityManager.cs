namespace CFE
{
    using UnityEngine;
    using Actions.EntityActions;
    using Extensions;

    class AbilityManager : MonoBehaviour
    {
        public static AbilityBehaviorData activeAbility;

        [SerializeField]
        AbilityIndicatorControl spellIndicator;

        [SerializeField]
        AbilityBehaviorData[] AbilityBehavior;

        int activeIndex;

        #region
        void Start()
        {

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

        private void OnAbilitySelectButtonClickEvent(int character, int ability)
        {
            Debug.Log(character + "," + ability);
            activeIndex = 4 * character + ability;
            spellIndicator.enabled = true;
            activeAbility = AbilityBehavior[activeIndex];
            spellIndicator.setIndicator(activeAbility);
        }

        private void OnAbilityCastEvent(TilePosition target, AbilityBehaviorData abilityData, EntityModel castingEntity)
        {
            if (abilityData.AoEType == AbilityAOE_Type.Radial)
                CastRadialAbility(target, abilityData, castingEntity);
        }

        private void CastRadialAbility(TilePosition target, AbilityBehaviorData abilityData, EntityModel castingEntity)
        {
            Vector2[] tilePositions = Vector2Helper.getRadial(abilityData.radius);

            for (int i = 0; i < tilePositions.Length; i++)
            {
                tilePositions[i] += target.tilePosition;
                GameObject obj = Instantiate(abilityData.abilityPrefab, tilePositions[i], Quaternion.identity) as GameObject;
                obj.GetComponent<ITileEffect>().Initialize();
            }

            for (int i = 0; i < Vector2Helper.getRadialTileCount(abilityData.radius); i++)
            {
            }
        }
    }
}
