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
        /// <param name="subMenuPrefab">The prefab to use for creating a Sub-Menu</param>
        /// <param name="buttonPrefab">The prefab to use for creating buttons.</param>
        /// <param name="actionMenu">The menu that this drawer is drawing on.</param>
        /// <returns>The created button on the root menu that opens the sub menu.</returns>
        public override CharacterButton Draw(ICommanderReadable drawTarget, Transform content, 
            CombatSubMenu subMenuPrefab, CharacterButton buttonPrefab, CharacterActionMenu actionMenu)
        {
            ListAction listAction = drawTarget as ListAction;

            // Create the button that opens the SubMenu
            CharacterButton subMenuButton = CreateButton(drawTarget, actionMenu, buttonPrefab);

            // Create the sub-menu
            CombatSubMenu subMenu = GameObject.Instantiate(subMenuPrefab, actionMenu.transform);
            InitializeSubMenu(buttonPrefab, subMenu, actionMenu, listAction, subMenuButton.UnityButton);
            subMenu.Unload();

            // Hookup so the button opens the sub menu.
            subMenuButton.AddEnabledListener((unused) => actionMenu.OpenSubMenu(subMenu));

            return subMenuButton;
        }

        #region Sub-Menu Creation

        /// <summary>
        /// Initializes an already created sub-menu.
        /// </summary>
        /// <param name="buttonPrefab">The prefab to use for creating buttons.</param>
        /// <param name="subMenu"></param>
        /// <param name="actionMenu">The menu that this drawer is drawing on.</param>
        /// <param name="listAction"></param>
        /// <param name="parentButton"></param>
        /// <returns>The created button on the root menu.</returns>
        internal void InitializeSubMenu(CharacterButton buttonPrefab, CombatSubMenu subMenu, 
            CharacterActionMenu actionMenu, ListAction listAction, Button parentButton)
        {
            if (listAction.SubActions.Length == 0)
            {
                throw new System.IndexOutOfRangeException("Cannot initialize sub-menu with a listAction with 0 " +
                    "sub-actions.");
            }

            // Purely for hierarchy organization
            string menuName = $"{(actionMenu.LoadedCharacter == null ? "" : actionMenu.LoadedCharacter.name)}" +
                $"{listAction.GetName()}SubMenu";

            Button[] buttons = ConstructButtons(buttonPrefab, listAction.SubActions, actionMenu);
            subMenu.Initialize(buttons[0], parentButton, buttons.Length, menuName);
        }

        /// <summary>
        /// Construct all the buttons within a given sub-menu.
        /// </summary>
        /// <param name="buttonPrefab">The prefab to use for creating buttons.</param>
        /// <param name="buttonData">The button data array to construct the buttons from.</param>
        /// <param name="actionMenu">The sub-menu that the buttons will belong to.</param>
        /// <returns></returns>
        internal Button[] ConstructButtons(CharacterButton buttonPrefab, ICommanderReadable[] buttonData, 
            CharacterActionMenu actionMenu)
        {
            Button[] createdButtons = new Button[buttonData.Length];

            for (int i = 0; i < buttonData.Length; i++)
            {
                createdButtons[i] = CreateButton(buttonData[i], actionMenu, buttonPrefab).UnityButton;
            }
            HookupButtonNavigation(createdButtons);

            return createdButtons;
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
