namespace CFE
{

    interface ITileEffect
    {
        void Initialize();
        void OnTickUpdate(Tick data);
        void Terminate();
    }
}
