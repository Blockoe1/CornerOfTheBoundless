/*****************************************************************************
// File Name : CharacterActionMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Player interface with the combat system that allows them to issue commands to characters.
*****************************************************************************/
using COTB.UI;
using CustomAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace COTB.Combat.UI
{
    [RequireComponent(typeof(RootMenu))]
    public class CharacterActionMenu : MonoBehaviour
    {
        [SerializeField] private CombatSubMenu subMenuPrefab;
        [SerializeField] private CombatButton combatButtonPrefab;
        [SerializeField] private UnityEvent OnActionSelected;

        private CombatSubMenu currentSkillsMenu;

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

        /// <summary>
        /// Loads a given character's data to the action menu.
        /// </summary>
        internal void LoadCharacter(CombatSubMenu skillMenu)
        {
            currentSkillsMenu = skillMenu;
        }

        #region Button Interface
        /// <summary>
        /// Opens the skills menu of the current character.
        /// </summary>
        public void OpenSkillsMenu()
        {
            rootMenu.OpenSubMenu(currentSkillsMenu);
        }
        #endregion

        #region Sub-Menu Creation
        /// <summary>
        /// Creates a sub-menu of this combat menu with auto-generated buttons.
        /// </summary>
        /// <param name="buttonData">The buttons to generate.</param>
        /// <returns>The created sub-menu that is created as a child of this object.</returns>
        internal CombatSubMenu CreateSubMenu(IButtonReadable[] buttonData, string menuName, Button parentButton)
        {
            CombatSubMenu subMenu = Instantiate(subMenuPrefab, transform);
            InitializeSubMenu(subMenu, buttonData, menuName, parentButton);
            return subMenu;
        }

        /// <summary>
        /// Initializes an already created sub-menu.
        /// </summary>
        /// <param name="subMenu"></param>
        /// <param name="buttonData"></param>
        /// <param name="menuName"></param>
        /// <param name="parentButton"></param>
        internal void InitializeSubMenu(CombatSubMenu subMenu, IButtonReadable[] buttonData, string menuName, Button parentButton)
        {
            Button[] buttons = ConstructButtons(buttonData, subMenu);
            subMenu.Initialize(buttons[0], parentButton, buttons.Length, menuName);
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
            CombatButton createdButton = Instantiate(combatButtonPrefab, parentMenu.Content);
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
