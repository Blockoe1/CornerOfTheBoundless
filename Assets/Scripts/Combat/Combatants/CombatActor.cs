/*****************************************************************************
// File Name : CombatActor.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Controls combatants taking actions in combat and ordering said actions by queue and slowmode.
*****************************************************************************/
using UnityEngine;
using COTB.Combat.Actions;

namespace COTB.Combat
{
    public class CombatActor : MonoBehaviour
    {
        [SerializeField] private CombatEntity debugTarget;

        /// <summary>
        /// Causes this combatant to perform a given command.
        /// </summary>
        public void PerformAction(CombatActionContext toPerform)
        {
            // Add the command's main function to the queue.
            CombatQueue.AddToQueue(toPerform.Action.CommandMain((toPerform.Targets ?? new CombatEntity[] { debugTarget }), this), this); 
        }
    }
}
