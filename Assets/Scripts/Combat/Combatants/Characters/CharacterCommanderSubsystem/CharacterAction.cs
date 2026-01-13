/*****************************************************************************
// File Name : CharacterAction.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/12/2026
//
// Brief Description : Abstract base class for a type of action that a character can perform in combat.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.Characters
{
    [System.Serializable]
    public abstract class CharacterAction
    {
        /// <summary>
        /// Allows this action to reference components on the character commander that this action is referenced by.
        /// </summary>
        /// <param name="ownedCommander"></param>
        internal virtual void Reset(CharacterCommander ownedCommander) { }
    }
}
