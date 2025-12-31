/*****************************************************************************
// File Name : CharacterSelector.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/31/2025
//
// Brief Description : Controls the player's selection of a certain character to act and is the main system that 
// controls when the player can act.
*****************************************************************************/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace COTB.Combat
{
    public class CharacterManager : MonoBehaviour
    {
        #region CONSTS
        private const string TOGGLE_CHARACTER_ACTION_NAME = "ToggleCharacter";
        #endregion

        private Character[] characters;

        private InputAction toggleCharacterAction;

        private int sCharIndex;

        #region Properties
        private int SelectedCharacterIndex
        {
            get { return characters.Length; }
            set
            {
                // Deselect the previosuly selected character.
                if (characters[sCharIndex] != null)
                {
                    characters[sCharIndex].SetSelected(false);
                }

                sCharIndex = value;
                LoopIndex(characters, ref sCharIndex);

                // Select the newly selected character.
                if (characters[sCharIndex] != null)
                {
                    characters[sCharIndex].SetSelected(true);
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
            characters = FindObjectsByType<Character>(FindObjectsSortMode.InstanceID);
        }

        /// <summary>
        /// Unsubscribe any stray event references.
        /// </summary>
        private void OnDestroy()
        {
            toggleCharacterAction.performed -= ToggleSelectedCharacter;
        }

        /// <summary>
        /// Begins the player's action by selecting the first available character.
        /// </summary>
        public void BeginPlayerAction()
        {
            // Select the first valid character.

            ToggleEnabled(true);
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
        /// Changes the currently selected character based on player input.
        /// </summary>
        private void ToggleSelectedCharacter(InputAction.CallbackContext obj)
        {
            int inputDir = System.MathF.Sign(obj.ReadValue<float>());

            // Toggle the selected character here.
            SelectedCharacterIndex += inputDir;
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
            if (index >= collection.Count())
            {
                didLoop = true;
                index -= collection.Count();
            }
            else if (index < 0)
            {
                didLoop = true;
                index += collection.Count();
            }
            return didLoop;
        }
    }
}
