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
    [CustomActionDrawer(typeof(ListAction))]
    public class SubMenuDrawer : ActionDrawer
    {
        /// <summary>
        /// Draws a button linking to the menu and the corresponding SubMenu.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is creating buttons for.</param>
        /// <param name="content">The GemaObject holding the content of the CharacterActionMenu</param>
        /// <returns>The created button on the root menu that opens the sub menu.</returns>
        public override CharacterButton Draw(ICommanderReadable drawTarget, Transform content)
        {
            ListAction listAction = drawTarget as ListAction;

            // Create the button that opens the SubMenu
            CharacterButton subMenuButton = SpawnButton(drawTarget, ActionMenu.Content);

            // Create the sub-menu
            CombatSubMenu subMenu = GameObject.Instantiate(SubMenuPrefab, ActionMenu.transform);
            SetupSubMenu(subMenu, listAction, subMenuButton.UnityButton);
            subMenu.Unload();

            // Hookup so the button opens the sub menu.
            subMenuButton.AddEnabledListener((unused) => ActionMenu.OpenSubMenu(subMenu));

            return subMenuButton;
        }

        #region Sub-Menu Creation

        /// <summary>
        /// Initializes an already created sub-menu.
        /// </summary>
        /// <param name="subMenu"></param>
        /// <param name="listAction"></param>
        /// <param name="parentButton"></param>
        /// <returns>The created button on the root menu.</returns>
        internal void SetupSubMenu(CombatSubMenu subMenu, ListAction listAction, Button parentButton)
        {
            if (listAction.SubActions.Length == 0)
            {
                throw new System.IndexOutOfRangeException("Cannot initialize sub-menu with a listAction with 0 " +
                    "sub-actions.");
            }

            // Purely for hierarchy organization
            string menuName = $"{(ActionMenu.LoadedCharacter == null ? "" : ActionMenu.LoadedCharacter.name)}" +
                $"{listAction.GetName()}SubMenu";
            subMenu.gameObject.name = menuName;

            Button[] buttons = CreateButtons(listAction.SubActions, subMenu.Content);
            subMenu.Initialize(buttons[0], parentButton, buttons.Length);
        }

        /// <summary>
        /// Construct all the buttons within a given sub-menu.
        /// </summary>
        /// <param name="buttonData">The button data array to construct the buttons from.</param>
        /// <param name="content">The transform that the created buttons will be children of.</param>
        /// <returns></returns>
        internal Button[] CreateButtons(ICommanderReadable[] buttonData, Transform content)
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
        protected CharacterButton DrawChild(ICommanderReadable subCommand, Transform content)
        {
            // Create a drawer for the sub-command.
            ActionDrawer drawer = GetActionDrawer(subCommand);
            drawer.Initialize(SubMenuPrefab, ButtonPrefab, ActionMenu);
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
