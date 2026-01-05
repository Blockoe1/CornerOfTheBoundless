/*****************************************************************************
// File Name : ApplyEffect.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Applies a combat effect to the target.
*****************************************************************************/
using System.Collections;
using UnityEngine;

namespace COTB.Combat
{
    [System.Serializable]
    public class ApplyEffect : CommandComponent
    {
        public override void ExecuteComponent(Combatant[] targets, CombatActor actor)
        {
            throw new System.NotImplementedException();
        }
    }
}
