/*****************************************************************************
// File Name : CharacterSelector.cs
// Author : Eli Koederitz
// Creation Date : 1/13/2025
// Last Modified : 1/13/2025
//
// Brief Description : Controls selecting which of the currently valid characters will be given a command.
*****************************************************************************/
using COTB.Combat.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace COTB.Combat.UI.CharacterMenu
{
    public class CharacterSelector : MonoBehaviour
    {
        #region CONSTS
        private const string TOGGLE_CHARACTER_ACTION_NAME = "ToggleCharacter";
        #endregion

        [SerializeField] private UnityEvent<CharacterCommander> OnCharacterSelected;

        private CharacterCommander[] characters;

        private InputAction toggleCharacterAction;

        private CharacterCommander selectedCharacter;
        private int sCharIndex;

        #region Properties
        private int SelectedCharacterIndex
        {
            get { return sCharIndex; }
            set
            {
                sCharIndex = value;
                LoopIndex(characters, ref sCharIndex);
            }
        }

        private CharacterCommander SelectedCharacter
        {
            get
            {
                return selectedCharacter;
            }
            set
            {
                // Deselect the previosuly selected character.
                if (selectedCharacter != null)
                {
                    selectedCharacter.OnDeselect();
                }

                selectedCharacter = value;

                // Select the newly selected character.
                if (selectedCharacter != null)
                {
                    //// Initialize the selected character if they haven't already.
                    //if (!selectedCharacter.HasInitialized)
                    //{
                    //    selectedCharacter.Initialize(this);
                    //}
                    selectedCharacter.OnSelect();
                }
            }
        }
        #endregion

        /// <summary>
        /// Setup input on awake.
        /// </summary>
        private void Awake()
        {
            toggleCharacterAction = InputSystem.actions.FindAction(TOGGLE_CHARACTER_ACTION_NAME);

            // Find all the characters in the encounter.
            characters = FindObjectsByType<CharacterCommander>(FindObjectsSortMode.InstanceID);
        }

        /// <summary>
        /// Unsubscribe any stray event references.
        /// </summary>
        private void OnDestroy()
        {
            toggleCharacterAction.performed -= ToggleSelectedCharacter;
        }

        /// <summary>
        /// Toggles if the character selector is enabled to swap the selected character or not.
        /// </summary>
        /// <param name="isEnabled"></param>
        private void ToggleEnabled(bool isEnabled)
        {
            if (isEnabled)
            {
                toggleCharacterAction.performed += ToggleSelectedCharacter;
            }
            else
            {
                toggleCharacterAction.performed -= ToggleSelectedCharacter;
            }
        }

        /// <summary>
        /// Begins the player's action by selecting the first available character.
        /// </summary>
        [ContextMenu("Begin player action")] // Debug
        public void BeginPlayerAction()
        {
            // Select the first valid character.
            Debug.Log(SelectedCharacterIndex);
            SelectedCharacter = characters[SelectedCharacterIndex];

            ToggleEnabled(true);
        }

        /// <summary>
        /// Changes the currently selected character based on player input.
        /// </summary>
        private void ToggleSelectedCharacter(InputAction.CallbackContext obj)
        {
            int inputDir = System.MathF.Sign(obj.ReadValue<float>());

            // Toggle the selected character here.
            SelectedCharacterIndex += inputDir;
            SelectedCharacter = characters[SelectedCharacterIndex];
        }

        /// <summary>
        /// Loops an index around if it exceeds the bounds of a collection.
        /// </summary>
        /// <typeparam name="T">The type stored in the collection.</typeparam>
        /// <param name="collection">THe collection to loop the index within.</param>
        /// <param name="index">The current value of the index.</param>
        /// <returns> The looped index value.</returns>
        public static bool LoopIndex<T>(IEnumerable<T> collection, ref int index)
        {
            bool didLoop = false;
            while (index >= collection.Count())
            {
                didLoop = true;
                index -= collection.Count();
            }
            while (index < 0)
            {
                didLoop = true;
                index += collection.Count();
            }
            return didLoop;
        }

        /// <summary>
        /// Causes the currently selected character to perform some combat action.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        //public void PerformActionOnSelectedCharacter(CombatActionData action)
        //{
        //    SelectedCharacter.PerformAction(action);
        //}
    }
}
