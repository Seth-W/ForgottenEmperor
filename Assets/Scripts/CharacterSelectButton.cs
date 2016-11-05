namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class CharacterSelectButton : MonoBehaviour
    {
        public delegate void CharacterSelectButtonClick(int button);
        public static CharacterSelectButtonClick CharacterSelectButtonClickEvent;

        [SerializeField]
        int PlayerIndex;

        public void OnClick()
        {
            CharacterSelectButtonClickEvent(PlayerIndex);
        }
    }
}