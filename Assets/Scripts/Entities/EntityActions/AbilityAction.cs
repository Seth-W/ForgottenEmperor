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
            CreatureResourceComponent resourceComponent = caster.GetComponent<CreatureResourceComponent>();
            if (resourceComponent.GetMana() < abilityData.manaCost)
                return true;
            resourceComponent.IncrementMana(-abilityData.manaCost);
            AbilityCastEvent(tilePos, abilityData, caster);
            return true;
        }

        public bool revert()
        {
            return true;
        }
    }
}
