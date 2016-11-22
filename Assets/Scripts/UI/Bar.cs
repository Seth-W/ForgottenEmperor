namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class Bar : MonoBehaviour
    {
        float fullXPos, fullXWidth;
        RectTransform rect;
        [SerializeField]
        int characterIndex;
        [SerializeField]
        bool HealthBar;

        #region MonoBehaviours
        void Start()
        {
            rect = (RectTransform)transform;
            fullXPos = rect.anchoredPosition.x;
            fullXWidth = rect.sizeDelta.x;
            if(characterIndex > 3)
            {
                if(HealthBar)
                    EntityManager.enemyModel.GetComponent<CreatureResourceComponent>().RegisterHealthBar(this);
                if (!HealthBar)
                    EntityManager.enemyModel.GetComponent<CreatureResourceComponent>().RegisterManaBar(this);
                return;
            }
            if (HealthBar)
                EntityManager.getPlayer(characterIndex).GetComponent<CreatureResourceComponent>().RegisterHealthBar(this);
            if(!HealthBar)
                EntityManager.getPlayer(characterIndex).GetComponent<CreatureResourceComponent>().RegisterManaBar(this);
        }

        void OnEnable()
        {

        }

        void OnDisable()
        {

        }
        #endregion

        public void SetPercentageFull(float percentage)
        {
            if(percentage > 1 || percentage < 0)
            {
                Debug.LogError("Did not pass a valid percentage parameter");
                return;
            }

            rect.sizeDelta = new Vector2(fullXWidth * percentage, rect.sizeDelta.y);
            float newXOffset = rect.sizeDelta.x;
            newXOffset = fullXWidth - rect.sizeDelta.x;
            newXOffset /= 2;
            rect.anchoredPosition = new Vector2(fullXPos - newXOffset, rect.anchoredPosition.y);
        }
    }
}