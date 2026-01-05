/*****************************************************************************
// File Name : Heal.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Command component for healing a target.
*****************************************************************************/
using System.Collections;
using UnityEngine;

namespace COTB.Combat
{
    [System.Serializable]
    public class Heal : CommandComponent
    {
        [SerializeField] private float healthMultiplier;

        /// <summary>
        /// Heals the target for a certain amount of health.
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="actor"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ExecuteComponent(Combatant[] targets, CombatActor actor)
        {
            throw new System.NotImplementedException();
        }
    }
}
