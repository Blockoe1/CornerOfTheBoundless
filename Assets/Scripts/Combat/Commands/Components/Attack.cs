/*****************************************************************************
// File Name : Attack.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Command component for directly attacking a target.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    [System.Serializable]
    public class Attack : CommandComponent
    {
        [SerializeField] private float attackMultiplier;

        /// <summary>
        /// Deals an attack to the target.
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="actor"></param>
        public override void ExecuteComponent(CombatTarget[] targets, CombatActor actor)
        {
            // Deal damage to all the passed in targets.
            foreach(CombatTarget target in targets)
            {
                // Using attack multiplier as a placeholder until I get stats figured out.
                target.TakeDamage((int)attackMultiplier);
                Debug.Log("Dealt " + attackMultiplier + " damage to target " + target.name);
                // TODO implement getting stats.
            }
        }
    }
}
