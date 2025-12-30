/*****************************************************************************
// File Name : CharacterSelector.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Controls the player's selection of a certain character to act and is the main system that 
// controls when the player can act.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

namespace COTB.Combat
{
    public class CharacterSelector : MonoBehaviour
    {
        #region CONSTS
        private const string TOGGLE_CHARACTER_ACTION_NAME = "ToggleCharacter";
        #endregion

        private InputAction toggleCharacterAction;

        /// <summary>
        /// Setup input on awake.
        /// </summary>
        private void Awake()
        {
            toggleCharacterAction = InputSystem.actions.FindAction(TOGGLE_CHARACTER_ACTION_NAME);
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
        }
    }
}
