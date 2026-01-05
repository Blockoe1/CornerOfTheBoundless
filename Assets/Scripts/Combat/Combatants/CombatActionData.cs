/*****************************************************************************
// File Name : CombatActionData.cs
// Author : Eli Koederitz
// Creation Date : 1/3/2025
// Last Modified : 1/3/2025
//
// Brief Description : Wrapper class that contains information about a combat action: the command used and the targets.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    public struct CombatActionData
    {
        private readonly Command toUse;
        private readonly Combatant[] targets;

        #region Properties
        public Command Command => toUse;
        public Combatant[] Targets => targets;
        #endregion

        public CombatActionData(Command toUse, Combatant[] targets)
        {
            this.toUse = toUse;
            this.targets = targets;
        }
    }
}
