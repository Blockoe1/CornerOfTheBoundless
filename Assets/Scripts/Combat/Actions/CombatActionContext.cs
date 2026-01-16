/*****************************************************************************
// File Name : CombatActionData.cs
// Author : Eli Koederitz
// Creation Date : 1/3/2025
// Last Modified : 1/3/2025
//
// Brief Description : Wrapper class that contains information about a combat action: the command used and the targets.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.Actions
{
    public struct CombatActionContext
    {
        private readonly CombatAction toUse;
        private readonly CombatEntity[] targets;

        #region Properties
        public CombatAction Action => toUse;
        public CombatEntity[] Targets => targets;
        #endregion

        public CombatActionContext(CombatAction toUse, CombatEntity[] targets)
        {
            this.toUse = toUse;
            this.targets = targets;
        }
    }
}
