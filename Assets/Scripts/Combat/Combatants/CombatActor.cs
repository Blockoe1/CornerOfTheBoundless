/*****************************************************************************
// File Name : CombatActor.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Controls combatants taking actions in combat and ordering said actions by queue and slowmode.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    public class CombatActor : MonoBehaviour
    {
        [SerializeField] private CombatEntity debugTarget;
        [SerializeField] private Command debugCommand;

        /// <summary>
        /// Causes this combatant to perform a given command.
        /// </summary>
        public void PerformCommand(Command toPerform, CombatEntity[] targets)
        {
            // Add the command's main function to the queue.
            targets ??= new CombatEntity[] { debugTarget }; 
            CombatQueue.AddToQueue(toPerform.CommandMain(targets, this), this);
        }

        #region Debug
        [ContextMenu("Perform Debug Command")]
        private void PerformDebugCommand()
        {
            PerformCommand(debugCommand, new CombatEntity[] { debugTarget } );
        }
        #endregion
    }
}
