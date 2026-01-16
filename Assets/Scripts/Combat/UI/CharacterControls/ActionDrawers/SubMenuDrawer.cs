/*****************************************************************************
// File Name : SubMenuDrawer.cs
// Author : Eli Koederitz
// Creation Date : 1/15/2026
// Last Modified : 1/15/2026
//
// Brief Description : ActionDrawer that draws sub-menus for list actions.
*****************************************************************************/
using COTB.Combat.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace COTB.Combat.UI.CharacterMenu
{
    [CustomCommandDrawer(typeof(ListCommand), typeof(SkillsCommand))]
    public class SubMenuDrawer : CommandDrawer
    {
        /// <summary>
        /// Draws a button linking to the menu and the corresponding SubMenu.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is creating buttons for.</param>
        /// <param name="content">The GemaObject holding the content of the CharacterActionMenu</param>
        /// <returns>The created button on the root menu that opens the sub menu.</returns>
        public override CommandButton Draw(ICommandReadable drawTarget, Transform content)
        {
            ListCommand listCommand = drawTarget as ListCommand;

            // Create the button that opens the SubMenu
            CommandButton subMenuButton = SpawnButton(drawTarget, CommandMenu.Content);

            // Create the sub-menu
            CommandSubMenu subMenu = GameObject.Instantiate(SubMenuPrefab, CommandMenu.transform);
            SetupSubMenu(subMenu, listCommand, subMenuButton.UnityButton);
            subMenu.Unload();

            // Hookup so the button opens the sub menu.
            subMenuButton.AddEnabledListener((unused) => CommandMenu.OpenSubMenu(subMenu));

            return subMenuButton;
        }

        #region Sub-Menu Creation

        /// <summary>
        /// Initializes an already created sub-menu.
        /// </summary>
        /// <param name="subMenu">The sub menu that is being setup.</param>
        /// <param name="listCommand">The list command whose sub-commands will be populatead into the sub menu.</param>
        /// <param name="parentButton">The parent button that opens this sub menu.</param>
        /// <returns>The created button on the root menu.</returns>
        internal void SetupSubMenu(CommandSubMenu subMenu, ListCommand listCommand, Button parentButton)
        {
            if (listCommand.SubCommands.Length == 0)
            {
                throw new System.IndexOutOfRangeException("Cannot initialize sub-menu with a listAction with 0 " +
                    "sub-actions.");
            }

            // Purely for hierarchy organization
            Debug.Log(CommandMenu.LoadedCharacter);
            string menuName = $"{(CommandMenu.LoadedCharacter == null ? "" : CommandMenu.LoadedCharacter.name)}" +
                $"{listCommand.GetName()}SubMenu";
            subMenu.gameObject.name = menuName;

            Button[] buttons = CreateButtons(listCommand.SubCommands, subMenu.Content);
            subMenu.Initialize(buttons[0], parentButton, buttons.Length);
        }

        /// <summary>
        /// Construct all the buttons within a given sub-menu.
        /// </summary>
        /// <param name="buttonData">The button data array to construct the buttons from.</param>
        /// <param name="content">The transform that the created buttons will be children of.</param>
        /// <returns></returns>
        internal Button[] CreateButtons(ICommandReadable[] buttonData, Transform content)
        {
            Button[] createdButtons = new Button[buttonData.Length];

            for (int i = 0; i < buttonData.Length; i++)
            {
                createdButtons[i] = DrawChild(buttonData[i], content).UnityButton;
            }
            HookupButtonNavigation(createdButtons);

            return createdButtons;
        }

        /// <summary>
        /// Draws a sub command of this list command.
        /// </summary>
        /// <param name="subCommand">The button data to construct the button from.</param>
        /// <param name="content">The transform to spawn the button as a child of.</param>
        /// <returns>The created button.</returns>
        protected CommandButton DrawChild(ICommandReadable subCommand, Transform content)
        {
            // Create a drawer for the sub-command.
            CommandDrawer drawer = GetCommandDrawer(subCommand);
            drawer.Initialize(SubMenuPrefab, ButtonPrefab, CommandMenu);
            return drawer.Draw(subCommand, content);
        }

        /// <summary>
        /// Sets up navigation between all buttons in a given list. 
        /// </summary>
        /// <param name="buttons"> The list of buttons that should navigate to each other. </param>
        private static void HookupButtonNavigation(Button[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                /// Finds the index of the next and previous buttons in the list, with looping.
                int prevIndex = i - 1;
                if (prevIndex < 0)
                {
                    prevIndex = buttons.Length - 1;
                }

                int nextIndex = i + 1;
                if (nextIndex >= buttons.Length)
                {
                    nextIndex = 0;
                }

                // TODO: Allow jumping mutliple buttons by navigating left or right.

                Navigation buttonNav = new Navigation();
                buttonNav.mode = Navigation.Mode.Explicit;
                buttonNav.selectOnUp = buttons[prevIndex];
                buttonNav.selectOnDown = buttons[nextIndex];
                buttons[i].navigation = buttonNav;
            }
        }
        #endregion
    }
}
