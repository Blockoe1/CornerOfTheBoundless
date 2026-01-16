/*****************************************************************************
// File Name : ActionDrawer.cs
// Author : Eli Koederitz
// Creation Date : 1/15/2025
// Last Modified : 1/15/2025
//
// Brief Description : Abstract base class that draws CharacterActions and other ICommanderReadable
// objects as buttons on the CharacterActionMenu.
*****************************************************************************/
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

namespace COTB.Combat.UI.CharacterMenu
{
    public abstract class ActionDrawer
    {
        private CombatSubMenu subMenuPrefab;
        private CharacterButton buttonPrefab;
        private CharacterActionMenu actionMenu;

        #region Properties
        protected virtual CombatSubMenu SubMenuPrefab => subMenuPrefab;
        protected virtual CharacterButton ButtonPrefab => buttonPrefab;
        protected virtual CharacterActionMenu ActionMenu => actionMenu;
        #endregion

        /// <summary>
        /// Sets the prefabs that this drawer uses.
        /// </summary>
        /// <param name="subMenuPrefab"></param>
        /// <param name="buttonPrefab"></param>
        public void Initialize(CombatSubMenu subMenuPrefab, CharacterButton buttonPrefab, CharacterActionMenu actionMenu)
        {
            this.buttonPrefab = buttonPrefab;
            this.subMenuPrefab = subMenuPrefab;
            this.actionMenu = actionMenu;
        }

        /// <summary>
        /// Draws a given ICommanderReadable on the CharacterActionMenu by spawning button prefabs.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is creating buttons for.</param>
        /// <param name="content">The GemaObject holding the content of the CharacterActionMenu</param>
        /// <param name="subMenuPrefab">The prefab to use for creating a Sub-Menu</param>
        /// <param name="buttonPrefab">The prefab to use for creating buttons.</param>
        /// <param name="actionMenu">The menu that this drawer is drawing on.</param>
        /// <returns>The created button on the root menu.</returns>
        public abstract CharacterButton Draw(ICommanderReadable drawTarget, Transform content);

        /// <summary>
        /// By default, ActionDrawers don't give a specific override.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is drawing.</param>
        /// <returns></returns>
        public virtual ICommanderReadable GetOverride(ICommanderReadable drawTarget)
        {
            return null;
        }

        /// <summary>
        /// Spawns a new button instance from this drawer's button prefab.
        /// </summary>
        /// <param name="drawTarget">The command to draw a button for.</param>
        /// <param name="content">The transform that the spawned button should be a child of.</param>
        /// <param name="actionMenu">The action menu this button belongs to.</param>
        protected CharacterButton SpawnButton(ICommanderReadable drawTarget, Transform content)
        {
            // Create a Drawer for the button data we're creating a button for.
            CharacterButton createdButton = GameObject.Instantiate(ButtonPrefab, content);
            createdButton.Initialize(drawTarget, ActionMenu);
            return createdButton;
        }

        /// <summary>
        /// Gets the correct ActionDrawer to use to draw this ICommanderReadable object.
        /// </summary>
        /// <param name="action">The action to get the drawer for.</param>
        /// <returns>The ActionDrawer that will draw the action.</returns>
        internal static ActionDrawer GetActionDrawer(ICommanderReadable action)
        {
            Type actionType = action.GetType();
            // Search through this assembly to find subclasses of ActionDrawer that
            // Implement the correct attribute for this actions type.
            foreach (Type type in Assembly.GetAssembly(typeof(ActionDrawer)).GetTypes())
            {
                if (!type.IsSubclassOf(typeof(ActionDrawer)) || type.IsAbstract)
                {
                    continue;
                }

                // Get the attribute storing type metadata from the ActionDrawer type
                if (Attribute.GetCustomAttribute(type, typeof(CustomActionDrawerAttribute))
                    is CustomActionDrawerAttribute cada)
                {
                    if (cada == null && cada.Types.Contains(type))
                    {
                        return Activator.CreateInstance(actionType) as ActionDrawer;
                    }
                }
            }
            return null;
        }
    }
}
