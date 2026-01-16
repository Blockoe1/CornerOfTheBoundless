/*****************************************************************************
// File Name : CharacterCommander.cs
// Author : Eli Koederitz
// Creation Date : 1/11/2026
// Last Modified : 1/15/2026
//
// Brief Description : Controls the set of commands and actions that each character can perform.
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;
using COTB.Combat.Actions;

namespace COTB.Combat.Characters
{
    public class CharacterCommander : CombatCommander
    {
        [Header("Character Commands")]
        //[SerializeField, Tooltip("")] private CommandTags lockedTags;
        [SerializeReference, ClassDropdown(typeof(CharacterCommand))] private CharacterCommand[] commands;
        [Header("Selection Events")]
        [SerializeField] private UnityEvent OnSelectEvent;
        [SerializeField] private UnityEvent OnDeselectEvent;

        private readonly List<Predicate<ICommandReadable>> lockPredicates = new();

        #region Properties
        public ReadOnlyCollection<CharacterCommand> Commands => Array.AsReadOnly(commands);
        #endregion

        /// <summary>
        /// Propogate reset calls to the CharacterActions so they can get references to components on this character.
        /// </summary>
        [ContextMenu("Get Component References")]
        protected override void GetComponents()
        {
            foreach(var command in commands)
            {
                command.GetComponents(this);
            }
        }

        /// <summary>
        /// Checks if a specific CharacterAction is locked for this character.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>True if this action is locked for this character.</returns>
        public bool CheckLocked(ICommandReadable command)
        {
            bool isLocked = false;
            foreach (Predicate<ICommandReadable> predicate in lockPredicates)
            {
                isLocked |= predicate(command);
            }
            return isLocked;
        }

        #region Selection
        /// <summary>
        /// Functions called by the CharacterSelector UI script that can notify this character it's been selected.
        /// </summary>
        public void OnSelect()
        {
            OnSelectEvent?.Invoke();
        }
        public void OnDeselect()
        {
            OnDeselectEvent?.Invoke();
        }
        #endregion

        /// <summary>
        /// Causes this combatant perform a command.
        /// </summary>
        /// <remarks>
        /// Just reroutes to the attached CombatActor on this GO.
        /// </remarks>
        /// <param name="action">The action for the character to perform.</param>
        public void PerformAction(CombatActionContext action)
        {
            Actor.PerformAction(action);
        }
    }
}
