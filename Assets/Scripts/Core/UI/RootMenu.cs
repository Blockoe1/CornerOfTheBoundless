/*****************************************************************************
// File Name : RootMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : The core menu opened from outside input that can have multiple sub-menus within it.
*****************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace COTB.UI
{
    public class RootMenu : Menu
    {
        #region CONSTS
        private const string CANCEL_INPUT_NAME = "Cancel";
        #endregion

        #region Vars
        [SerializeField, Tooltip("If true, then pressing cancel while at this root menu will close it.")] 
        private bool canCloseSelf; 
        /// <summary>
        /// Input
        /// </summary>
        private InputAction cancelAction;

        // Stores the sub-menus opened on this menu.
        private readonly List<SubMenu> menuHierarchy = new List<SubMenu>();
        #endregion

        /// <summary>
        /// Setup input
        /// </summary>
        private void Awake()
        {
            cancelAction = InputSystem.actions.FindAction(CANCEL_INPUT_NAME);
        }
        private void OnEnable()
        {
            cancelAction.performed += CancelAction_Performed;
        }
        private void OnDisable()
        {
            cancelAction.performed -= CancelAction_Performed;
        }

        /// <summary>
        /// When the base menu is unloaded, close all sub-menus as well.
        /// </summary>
        public override void Unload()
        {
            base.Unload();
            CloseAllMenus();
        }

        #region Sub-Menu Management
        /// <summary>
        /// Opens a sub-menu on top of this root menu.
        /// </summary>
        /// <param name="subMenu">The sub-menu to open.</param>
        public void OpenSubMenu(SubMenu subMenu)
        {
            subMenu.Load();
            menuHierarchy.Add(subMenu);
        }

        /// <summary>
        /// Close the most recently opened menu when the player inputs cancel.
        /// </summary>
        protected virtual void CancelAction_Performed(InputAction.CallbackContext obj)
        {
            if (menuHierarchy.Count > 0)
            {
                CloseRecentMenu();
            }
            // Close this menu if canCloseSelf is set.
            else if (canCloseSelf)
            {
                Unload();
            }
        }

        /// <summary>
        /// Closes the most recently opened sub-menu.
        /// </summary>
        private void CloseRecentMenu()
        {
            if (menuHierarchy.Count == 0)
            {
                return;
            }
            menuHierarchy[^1].Unload();
        }

        /// <summary>
        /// Closes all currently opened sub menus in the menu hierarchy.
        /// </summary>
        public void CloseAllMenus()
        {
            EventSystem.current.SetSelectedGameObject(null);
            foreach (SubMenu subMenu in menuHierarchy)
            {
                subMenu.ToggleMenu(false);
            }
            menuHierarchy.Clear();
        }
        #endregion
    }
}