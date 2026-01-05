/*****************************************************************************
// File Name : CharacterActionMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Player interface with the combat system that allows them to issue commands to characters.
*****************************************************************************/
using COTB.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace COTB.Combat.UI.CharacterControls
{
    [RequireComponent(typeof(RootMenu))]
    public class CharacterActionMenu : MonoBehaviour
    {
        #region CONSTS
        private const string TOGGLE_CHARACTER_ACTION_NAME = "ToggleCharacter";
        #endregion

        [SerializeField] private ScrollWithSelected scrollController;

        private CharacterCommander[] characters;

        private InputAction toggleCharacterAction;

        private CharacterCommander selectedCharacter;
        private int sCharIndex;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private RootMenu rootMenu;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            rootMenu = GetComponent<RootMenu>();
        }
        #endregion

        #region Properties
        public Transform Content => rootMenu.Content;
        public ScrollWithSelected ScrollController => scrollController;
        public RootMenu RootMenu => rootMenu;
        #endregion

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
                   // Initialize the selected character if they haven't already.
                   if (!selectedCharacter.HasInitialized)
                   {
                        selectedCharacter.Initialize(this);
                   }
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
        public void PerformActionOnSelectedCharacter(CombatActionData action)
        {
            SelectedCharacter.PerformAction(action);
        }
    }
}
