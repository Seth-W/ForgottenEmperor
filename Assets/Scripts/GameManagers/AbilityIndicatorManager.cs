namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class AbilityIndicatorManager : MonoBehaviour
    {
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
            activeIndex = 4 * character + ability;
            spellIndicator.enabled = true;
            //spellIndicator.setIndicator(AbilityBehavior[4 * character + ability], EntityManager.activePlayer.transform, InputManager.currentFrameInputData.tilePos);
        }
    }
}
