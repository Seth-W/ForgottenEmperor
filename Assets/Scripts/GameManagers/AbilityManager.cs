namespace CFE
{
    using UnityEngine;
    using System.Collections;

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
        }

        void OnDisable()
        {
            AbilitySelectButton.AbilitySelectButtonClickEvent -= OnAbilitySelectButtonClickEvent;
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
    }
}
