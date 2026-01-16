/*****************************************************************************
// File Name : CharacterCommandMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Player interface with the combat system that allows them to issue commands to characters.
*****************************************************************************/
using COTB.Combat.Characters;
using COTB.Combat.Actions;
using COTB.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    public class CharacterCommandMenu : RootMenu
    {
        [SerializeField] private CommandSubMenu defaultSubMenuPrefab;
        [SerializeField] private CommandButton defaultButtonPrefab;

        private readonly Dictionary<CharacterCommander, CharacterMenuContext> characterMenus = new();

        internal event Action<CharacterCommander> OnMenuRefreshed;

        private CharacterCommander loadedCharacter;

        #region Properties
        public CharacterCommander LoadedCharacter => loadedCharacter;
        #endregion

        #region Nested
        private readonly struct CharacterMenuContext
        {
            internal readonly CommandButton[] menuButtons;
            internal readonly ICommandReadable[] buttonOverrides;

            internal CharacterMenuContext(CommandButton[] menuButtons, ICommandReadable[] buttonOverrides)
            {
                this.menuButtons = menuButtons;
                this.buttonOverrides = buttonOverrides;
            }
        }
        #endregion

        #region Loading/Unloading
        /// <summary>
        /// Loads the action menu for this character.
        /// </summary>
        /// <param name="character"></param>
        public void LoadCharacterMenu(CharacterCommander character)
        {
            // Attempting to load a null character closes the menu.
            if (character == null)
            {
                Unload();
                return;
            }

            loadedCharacter = character;

            if (!characterMenus.ContainsKey(character))
            {
                // If the menu for this character hasn't been created, create it.
                characterMenus.Add(character, CreateCharacterMenu(character));
            }

            // Load the full menu if it isnt already loaded.
            if (!IsLoaded)
            {
                Load();
            }
            LoadButtons(characterMenus[character]);
            Refresh(character);
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
        #endregion

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
            List<CommandButton> buttons = new List<CommandButton>();
            List<ICommandReadable> overrides = new List<ICommandReadable>();

            // Loop through each action and create a corresponding button and override for it.
            foreach (CharacterCommand command in character.Commands)
            {
                CommandDrawer drawer = CommandDrawer.GetCommandDrawer(command);
                if (drawer != null)
                {
                    drawer.Initialize(defaultSubMenuPrefab, defaultButtonPrefab, this);
                    buttons.Add(drawer.Draw(command, Content));
                    overrides.Add(drawer.GetOverride(command));
                }
            }

            List<CommandButton> returnButtons = new List<CommandButton>();
            List<ICommandReadable> returnOverrides = new List<ICommandReadable>();
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

        public void OnActionSelected(CombatAction selectedAction)
        {
            // Ignore null commands.
            if (selectedAction == null) { return; }
            // Implement stuff here.
            loadedCharacter.PerformAction(new CombatActionContext(selectedAction, null));
            // Unload after selecting a command for now.
            Unload();
        }
    }
}
