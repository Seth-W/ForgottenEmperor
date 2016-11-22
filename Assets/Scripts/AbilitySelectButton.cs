namespace CFE
{
    using UnityEngine;

    class AbilitySelectButton : MonoBehaviour
    {
        public delegate void AbilitySelectButtonClick(int characterIndex, string abilityName);
        public static AbilitySelectButtonClick AbilitySelectButtonClickEvent;

        [SerializeField]
        string abilityName;
        [SerializeField]
        int characterIndex;

        public void OnClick()
        {
            AbilitySelectButtonClickEvent(characterIndex, abilityName);
        }
    }
}
