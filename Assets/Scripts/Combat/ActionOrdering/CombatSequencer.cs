/*****************************************************************************
// File Name : CombatSequencer.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Controls the sequencing of combatant actions and coordinately between player ane enemy.
*****************************************************************************/
using System.Collections.Generic;

namespace COTB.Combat
{
    public class CombatSequencer
    {
        private readonly List<SlowmodeController> slowmodeConts = new();
        private bool flagNull;

        #region Properties
        public bool FLagNull => flagNull;
        #endregion

        public CombatSequencer()
        {
            CombatActor.OnQueueComplete += UpdateSlowmode;
        }

        /// <summary>
        /// Register/Unregister slowmode controllers.
        /// </summary>
        /// <param name="controller"></param>
        internal void RegisterController(SlowmodeController controller)
        {
            slowmodeConts.Add(controller);
        }
        internal void UnregisterController(SlowmodeController controller)
        {
            slowmodeConts.Remove(controller);
            // If there are no more registered slowmode controllers, then this sequencer is no longer needed and can
            // be cleaned up.
            if (slowmodeConts.Count == 0)
            {
                CleanUp();
            }
        }

        /// <summary>
        /// Updates the slowmode of all registered SlowmodeControllers 
        /// </summary>
        private void UpdateSlowmode()
        {
            foreach(SlowmodeController controller in slowmodeConts)
            {
                // Update slowmode for each controller.
                controller.UpdateSlowmode();
            }

            // 3 potential outcomes after updating slowmode:
            // - Monster does a command
            // - Player does a command
            // - Tick down again.
        }

        /// <summary>
        /// Cleans up event references when this object is no-longer needed.
        /// </summary>
        private void CleanUp()
        {
            CombatActor.OnQueueComplete -= UpdateSlowmode;
            flagNull = true;
        }
    }
}
