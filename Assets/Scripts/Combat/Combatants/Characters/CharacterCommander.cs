/*****************************************************************************
// File Name : CharacterCommander.cs
// Author : Eli Koederitz
// Creation Date : 1/11/2026
// Last Modified : 1/11/2026
//
// Brief Description : Controls the set of commands and actions that each character can perform.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.Characters
{
    public class CharacterCommander : CombatCommander
    {
        [Header("Character Actions")]
        [SerializeField, Tooltip("")] private CommandTags lockedTags;
        [SerializeReference, ClassDropdown(typeof(CharacterAction))] private CharacterAction[] actions;

        #region Properties
        public CommandTags LockedTags 
        { 
            get { return lockedTags; } 
            set { lockedTags = value; }
        }
        
        #endregion

        /// <summary>
        /// Propogate reset calls to the CharacterActions so they can get references to components on this character.
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
            foreach(var action in actions)
            {
                action.Reset(this);
            }
        }
    }
}
