namespace CFE
{
    using UnityEngine;

    class AbilitySelectButton : MonoBehaviour
    {
        public delegate void AbilitySelectButtonClick(int character, int button);
        public static AbilitySelectButtonClick AbilitySelectButtonClickEvent;

        [SerializeField]
        int abilityIndex, characterIndex;

        public void OnClick()
        {
            AbilitySelectButtonClickEvent(characterIndex, abilityIndex);
        }
    }
}
