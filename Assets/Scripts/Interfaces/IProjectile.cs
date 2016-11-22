namespace CFE
{
    using UnityEngine;

    interface IProjectile
    {
        void Initiate(TilePosition startPos, TilePosition endPos, StatBlock casterStats);
        void OnTickUpdate(Tick tickData);
        void Terminate();
    }
}