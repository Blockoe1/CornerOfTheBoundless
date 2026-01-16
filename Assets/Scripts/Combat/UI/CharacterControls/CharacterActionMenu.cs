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
    public class CharacterActionMenu : RootMenu
    {
        [SerializeField] private CombatSubMenu defaultSubMenuPrefab;
        [SerializeField] private CharacterButton defaultButtonPrefab;
        [SerializeField] private ScrollWithSelected scrollController;

        private readonly Dictionary<CharacterCommander, CharacterMenuContext> characterMenus = new();

        internal event Action<CharacterCommander> OnMenuRefreshed;

        private CharacterCommander loadedCharacter;

        #region Properties
        public ScrollWithSelected ScrollController => scrollController;
        public CharacterCommander LoadedCharacter => loadedCharacter;
        #endregion

        #region Nested
        private readonly struct CharacterMenuContext
        {
            internal readonly CharacterButton[] menuButtons;
            internal readonly ICommanderReadable[] buttonOverrides;

            internal CharacterMenuContext(CharacterButton[] menuButtons, ICommanderReadable[] buttonOverrides)
            {
                this.menuButtons = menuButtons;
                this.buttonOverrides = buttonOverrides;
            }
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
                characterMenus.Add(character, CreateCharacterMenu(character));
            }

            Load();
            LoadButtons(characterMenus[character]);

            loadedCharacter = character;
            Refresh(character);
        }

        /// <summary>
        /// Unloads the currently loaded character's menu.
        /// </summary>
        public override void Unload()
        {
            // Do nothing if an unload is attempted and there is no character loaded.
            if (loadedCharacter != null && characterMenus.ContainsKey(loadedCharacter))
            {
                CharacterMenuContext context = characterMenus[loadedCharacter];
                for (int i = 0; i < context.menuButtons.Length; i++)
                {
                    context.menuButtons[i].gameObject.SetActive(false);
                }
            }

            base.Unload();
        }

        /// <summary>
        /// Updates the base CharacterActionMenu based on a given CharacterMenuContext.
        /// </summary>
        /// <param name="context">The context to load.</param>
        private void LoadButtons(CharacterMenuContext context)
        {
            for (int i = 0; i < context.menuButtons.Length; i++)
            {
                context.menuButtons[i].gameObject.SetActive(true);
                // Orders the buttons in the menu based on their order in the array.
                context.menuButtons[i].transform.SetSiblingIndex(i);
                if (context.buttonOverrides[i] != null)
                {
                    context.menuButtons[i].ReadableData = context.buttonOverrides[i];
                }
            }
        }

        /// <summary>
        /// Refreshes using the most recently loaded character.
        /// </summary>
        public void Refresh()
        {
            Refresh(loadedCharacter);
        }
        /// <summary>
        /// Refreshes the action menu to ensure all buttons are up to date.
        /// </summary>
        /// <param name="character"></param>
        private void Refresh(CharacterCommander character)
        {
            OnMenuRefreshed?.Invoke(character);
        }

        /// <summary>
        /// Creates a new MenuContext for the given character.
        /// </summary>
        private CharacterMenuContext CreateCharacterMenu(CharacterCommander character)
        {
            List<CharacterButton> buttons = new List<CharacterButton>();
            List<ICommanderReadable> overrides = new List<ICommanderReadable>();

            // Loop through each action and create a corresponding button and override for it.
            foreach (CharacterAction action in character.Actions)
            {
                ActionDrawer drawer = ActionDrawer.GetActionDrawer(action);
                if (drawer != null)
                {
                    buttons.Add(drawer.Draw(action, Content, defaultSubMenuPrefab, defaultButtonPrefab, this));
                    overrides.Add(drawer.GetOverride(action));
                }
            }

            List<CharacterButton> returnButtons = new List<CharacterButton>();
            List<ICommanderReadable> returnOverrides = new List<ICommanderReadable>();
            // Convert the button lists to arrays, ignoring null buttons.
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] != null)
                {
                    returnButtons.Add(buttons[i]);
                    returnOverrides.Add(overrides[i]);
                }
            }
            CharacterMenuContext context = new CharacterMenuContext(returnButtons.ToArray(), returnOverrides.ToArray());
            return context;
        }

        public void OnCommandSelected(Command selectedCommand)
        {
            // Ignore null commands.
            if (selectedCommand == null) { return; }
            // Implement stuff here.
            loadedCharacter.PerformCommand(new CombatAction(selectedCommand, null));
        }
    }
}
