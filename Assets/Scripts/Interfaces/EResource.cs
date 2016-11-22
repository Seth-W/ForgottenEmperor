namespace CFE
{
    interface EResource
    {
        void Initiate();
        void IncrementHealth(float incrementVal, DamageType type);
        void IncrementMana(float incrementVal);
        float GetHealth();
        float GetMana();
    }
}
