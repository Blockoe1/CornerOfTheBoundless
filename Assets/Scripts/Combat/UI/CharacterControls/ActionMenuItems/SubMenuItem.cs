/*****************************************************************************
// File Name : SubMenuItem.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2026
// Last Modified : 1/4/2026
//
// Brief Description : Abstract class that creates an action menu item that opens a further sub-menu with a set of 
// configured buttons.
*****************************************************************************/
using COTB.UI;
using UnityEngine;
using UnityEngine.UI;

namespace COTB.Combat.UI.CharacterControls
{
    public abstract class SubMenuItem : ActionMenuItem
    {
        [SerializeField] private CombatSubMenu subMenuPrefab;
        [SerializeField] private CombatButton subMenuButtonPrefab;

        private SubMenu subMenu;
        protected RootMenu rootMenu;

        /// <summary>
        /// Gets the data to construct the sub menu's button's from.
        /// </summary>
        /// <returns></returns>
        protected abstract IButtonReadable[] GetButtonData();

        /// <summary>
        /// Initializes the sub-menu on the actionMenu
        /// </summary>
        /// <param name="actionMenu"></param>
        public override void Initialize(CharacterActionMenu actionMenu, CharacterCommander commander)
        {
            base.Initialize(actionMenu, commander);
            rootMenu = actionMenu.RootMenu;

            // Create the SubMenu
            subMenu = CreateSubMenu(GetButtonData(), actionMenu.transform, baseButton.LinkedButton);
        }

        /// <summary>
        /// Cleans up the sub menu
        /// </summary>
        public override void CleanUp()
        {
            base.CleanUp();
            // Destroy the sub-menu.
            Destroy(subMenu);
        }

        /// <summary>
        /// When a sub menu button is clicked, the corresponding sub menu should be opened.
        /// </summary>
        public override void OnButtonClicked()
        {
            rootMenu.OpenSubMenu(subMenu);
        }

        #region Sub-Menu Creation
        /// <summary>
        /// Creates a sub-menu of this combat menu with auto-generated buttons.
        /// </summary>
        /// <param name="buttonData">The buttons to generate.</param>
        /// <param name="menuParent">The parent GameObject to spawn the menu on.</param>
        /// <param name="parentButton">The parent button that opens this sub-menu.</param>
        /// <returns>The created sub-menu that is created as a child of this object.</returns>
        internal CombatSubMenu CreateSubMenu(IButtonReadable[] buttonData, Transform menuParent, Button parentButton)
        {
            CombatSubMenu subMenu = Instantiate(subMenuPrefab, menuParent);
            InitializeSubMenu(subMenu, buttonData, parentButton);
            subMenu.Unload();
            return subMenu;
        }

        /// <summary>
        /// Initializes an already created sub-menu.
        /// </summary>
        /// <param name="subMenu"></param>
        /// <param name="buttonData"></param>
        /// <param name="menuName"></param>
        /// <param name="parentButton"></param>
        internal void InitializeSubMenu(CombatSubMenu subMenu, IButtonReadable[] buttonData, Button parentButton)
        {
            if (buttonData == null || buttonData.Length == 0)
            {
                throw new System.IndexOutOfRangeException("Cannot initialize sub-menu with a null or 0 count buttonData array.");
            }

            Button[] buttons = ConstructButtons(buttonData, subMenu);
            subMenu.Initialize(buttons[0], parentButton, buttons.Length, name + buttonName + "SubMenu");
        }

        /// <summary>
        /// Construct all the buttons within a given sub-menu.
        /// </summary>
        /// <param name="buttonData">The button data array to construct the buttons from.</param>
        /// <param name="parentMenu">The sub-menu that the buttons will belong to.</param>
        /// <returns></returns>
        internal Button[] ConstructButtons(IButtonReadable[] buttonData, CombatSubMenu parentMenu)
        {
            Button[] createdButtons = new Button[buttonData.Length];

            for (int i = 0; i < buttonData.Length; i++)
            {
                createdButtons[i] = ConstructButton(buttonData[i], parentMenu);
            }
            HookupButtonNavigation(createdButtons);

            return createdButtons;
        }

        /// <summary>
        /// Constructs a specific combat button from the given button data.
        /// </summary>
        /// <param name="buttonData">The button data to construct the button from.</param>
        /// <param name="parentMenu">The SubMenu this button belongs to.</param>
        /// <returns>The created button.</returns>
        private Button ConstructButton(IButtonReadable buttonData, CombatSubMenu parentMenu)
        {
            CombatButton createdButton = Instantiate(subMenuButtonPrefab, parentMenu.Content);
            createdButton.Initialize(buttonData, parentMenu.ScrollController, parentMenu);
            return createdButton.LinkedButton;
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
