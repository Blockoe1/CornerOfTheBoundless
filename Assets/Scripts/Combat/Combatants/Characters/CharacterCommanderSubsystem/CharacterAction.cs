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
        [SerializeField, HideInInspector] private CharacterCommander commander;

        private bool isCheckingState;

        #region Properties
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Sprite Icon { get; }
        public abstract CommandTags Tags { get; }

        public virtual ActionState State
        {
            get
            {
                // Safeguard in case State is checked within an existing state check, because LockPredicates check
                // the action.
                if (isCheckingState) { return ActionState.Enabled; }

                isCheckingState = true;
                ActionState returnValue;
                if (IsDisabled)
                {
                    returnValue = ActionState.Disabled;
                }
                else if (IsLocked)
                {
                    returnValue = ActionState.Locked;
                }
                else
                {
                    returnValue = ActionState.Enabled;
                }
                isCheckingState = false;
                return returnValue;
            }
        }
        protected abstract bool IsDisabled { get; }
        protected bool IsLocked
        {
            get
            {
                return commander != null && commander.CheckLocked(this);
            }
        }

        protected CharacterCommander Commander => commander;
        #endregion

        /// <summary>
        /// Allows this action to reference components on the character commander that this action is referenced by.
        /// </summary>
        /// <param name="ownedCommander"></param>
        internal virtual void Reset(CharacterCommander ownedCommander) { }

        /// <summary>
        /// Stores a referenec to the CharacterCommander referencing this action.
        /// </summary>
        /// <param name="ownedCommander">The CharacterCommander this action belongs to.</param>
        internal virtual void OnValidate(CharacterCommander ownedCommander)
        {
            commander = ownedCommander;
        }

        /// <summary>
        /// Has the combatant this action belongs to perform this action.
        /// </summary>
        //public abstract void PerformAction();
    }
}
