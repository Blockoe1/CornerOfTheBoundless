/*****************************************************************************
// File Name : CharacterActionMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Player interface with the combat system that allows them to issue commands to characters.
*****************************************************************************/
using COTB.Combat.Characters;
using COTB.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    [RequireComponent(typeof(RootMenu))]
    public class CharacterActionMenu : MonoBehaviour
    {
        [SerializeField] private ScrollWithSelected scrollController;

        private readonly Dictionary<CharacterCommander, CharacterMenuContext> characterMenus = new();

        private event Action<CharacterCommander> OnMenuRefreshed;

        private CharacterCommander recentCharacter;

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

        #region Nested
        private class CharacterMenuContext
        {
            private CharacterButton[] menuButtons;
        }
        #endregion

        /// <summary>
        /// Loads the action menu for this character.
        /// </summary>
        /// <param name="character"></param>
        public void LoadCharacterMenu(CharacterCommander character)
        {
            if (!characterMenus.ContainsKey(character))
            {
                // If the menu for this character hasn't been created, create it.
            }

            // Load the menu context associated with this character.

            recentCharacter = character;
            Refresh(character);
        }

        /// <summary>
        /// Refreshes using the most recently loaded character.
        /// </summary>
        public void Refresh()
        {
            Refresh(recentCharacter);
        }
        /// <summary>
        /// Refreshes the action menu to ensure all buttons are up to date.
        /// </summary>
        /// <param name="character"></param>
        private void Refresh(CharacterCommander character)
        {
            OnMenuRefreshed?.Invoke(character);
        }
    }
}
