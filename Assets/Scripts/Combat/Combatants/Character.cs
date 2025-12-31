/*****************************************************************************
// File Name : Character.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Represents a player controlled character in combat.
*****************************************************************************/
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat
{
    public class Character : Combatant
    {
        [SerializeField] private Command[] debugSkills;

        #region Properties
        private Command[] Skills
        {
            get
            {
                return debugSkills;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSelected"></param>
        public void SetSelected(bool isSelected)
        {

        }
    }
}
