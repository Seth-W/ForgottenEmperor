namespace CFE
{
    using UnityEngine;

    interface IProjectile
    {
        void Initiate(TilePosition startPos, TilePosition endPos);
        void OnTickUpdate(Tick tickData);
        void Terminate();
    }
}