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
        [SerializeReference, HideInInspector] private CharacterCommander commander;

        private bool isCheckingState;

        #region Properties
        public string Name => GetNameRelative(-1);
        public string Description => GetDescriptionRelative(-1);
        public Sprite Icon => GetIconRelative(-1);
        public CommandTags Tags => GetTagsRelative(-1);

        public ActionState State => GetStateRelative(-1);

        protected CharacterCommander Commander => commander;
        #endregion

        public CharacterAction(CharacterCommander commander)
        {
            this.commander = commander;
        }

        /// <summary>
        /// Allows this action to reference components on the character commander that this action is referenced by.
        /// </summary>
        /// <param name="ownedCommander"></param>
        internal virtual void GetComponents(CharacterCommander ownedCommander) { }

        /// <summary>
        /// Stores a referenec to the CharacterCommander referencing this action.
        /// </summary>
        /// <param name="ownedCommander">The CharacterCommander this action belongs to.</param>
        internal virtual void OnValidate(CharacterCommander ownedCommander)
        {
            commander = ownedCommander;
        }

        #region Button API
        // Each Getter for a property takes in an index value so that buttons can
        // get different info based on their index.  -1 Is always the parent action.
        public abstract void PerformAction(int index);
        public abstract string GetNameRelative(int index);
        public abstract string GetDescriptionRelative(int index);
        public abstract Sprite GetIconRelative(int index);
        public abstract CommandTags GetTagsRelative(int index);
        public virtual ActionState GetStateRelative(int index)
        {
            // Safeguard in case State is checked within an existing state check, because LockPredicates check
            // the action.
            if (isCheckingState) { return ActionState.Enabled; }

            isCheckingState = true;
            ActionState returnValue;
            if (IsDisabledRelative(index))
            {
                returnValue = ActionState.Disabled;
            }
            else if (IsLockedRelative(index))
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
        protected abstract bool IsDisabledRelative(int index);
        protected abstract bool IsLockedRelative(int index);

        #endregion
    }
}
