namespace CFE
{

    interface ITileEffect
    {
        void Initialize(StatBlock castingEntityStat);
        void OnTickUpdate(Tick data);
        void Terminate();
    }
}
