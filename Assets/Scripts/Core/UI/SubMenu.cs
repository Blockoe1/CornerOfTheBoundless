/*****************************************************************************
// File Name : SubMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : A sub-menu opened from a root menu with the ability to close with the cancel button.
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace COTB.UI
{
    /// <summary>
    /// An child SubMenu of a base menu that can be opened and closed by a button.
    /// </summary>
    public class SubMenu : Menu
    {
        #region Vars
        [SerializeField, Tooltip("The button that opens this sub menu.")]
        protected Button parentButton;
        #endregion

        //private void OnDestroy()
        //{
        //    m_baseMenu.ExitMenuEvent -= CloseMenu;
        //}

        ///// <summary>
        ///// Initializes the object with key information.
        ///// </summary>
        ///// <remarks>
        ///// Should be called whenever this object is created. 
        ///// </remarks>
        ///// <param name="initButton"> The initially selected button when the menu is opened. </param>
        ///// <param name="numberOfButtons"> The number of buttons in this menu. </param>
        ///// <param name="menuName"> The name of the menu.  Used for hierarchy organization only. </param>
        ///// <param name="parentButton"> The button that opens this sub menu. </param>
        ///// <param name="baseMenu"> The base menu that this sub menu belongs to. </param>
        //public void Initialize(Button initButton, int numberOfButtons, string menuName, Button parentButton, RootMenu baseMenu)
        //{
        //    base.Initialize(initButton, numberOfButtons, menuName);
        //    m_parentButton = parentButton;
        //    m_baseMenu = baseMenu;

        //    m_baseMenu.ExitMenuEvent += CloseMenu;
        //}

        /// <summary>
        /// Instead of deselecting after unloading a sub-menu, select the parent button that opened this menu.
        /// </summary>
        public override void Unload()
        {
            parentButton.Select();
            ToggleMenu(false);
        }
    }
}