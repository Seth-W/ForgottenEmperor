namespace CFE.Actions.EntityActions
{
    using UnityEngine;

    class AbilityAction : IAction
    {
        public delegate void AbilityCast(TilePosition tilePos, AbilityBehaviorData data, EntityModel model);
        public static AbilityCast AbilityCastEvent;

        TilePosition tilePos;
        AbilityBehaviorData abilityData;
        EntityModel caster;

        public AbilityAction(TilePosition targetPos, AbilityBehaviorData data, EntityModel modelCasting)
        {
            tilePos = targetPos;
            abilityData = data;
            caster = modelCasting;
        }

        public bool cancel()
        {
            return true;
        }

        public bool execute()
        {
            Debug.Log("Executing an ability");
            AbilityCastEvent(tilePos, abilityData, caster);
            return true;
        }

        public bool revert()
        {
            return true;
        }
    }
}
