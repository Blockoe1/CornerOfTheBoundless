/*****************************************************************************
// File Name : CommandDrawer.cs
// Author : Eli Koederitz
// Creation Date : 1/15/2025
// Last Modified : 1/15/2025
//
// Brief Description : Abstract base class that draws CharacterCommands and other ICommandReadable
// objects as buttons on the CharacterCommandMenu.
*****************************************************************************/
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    public abstract class CommandDrawer
    {
        private CommandSubMenu subMenuPrefab;
        private CommandButton buttonPrefab;
        private CharacterCommandMenu commandMenu;

        #region Properties
        protected virtual CommandSubMenu SubMenuPrefab => subMenuPrefab;
        protected virtual CommandButton ButtonPrefab => buttonPrefab;
        protected virtual CharacterCommandMenu CommandMenu => commandMenu;
        #endregion

        /// <summary>
        /// Sets up this drawer.
        /// </summary>
        /// <param name="subMenuPrefab">The prefab for this drawer to use for sub-menus.</param>
        /// <param name="buttonPrefab">The prefab for this drawer to use for buttons.</param>
        /// <param name="commandMenu">The command menu this drawer is drawing on.</param>
        public void Initialize(CommandSubMenu subMenuPrefab, CommandButton buttonPrefab, CharacterCommandMenu commandMenu)
        {
            this.buttonPrefab = buttonPrefab;
            this.subMenuPrefab = subMenuPrefab;
            this.commandMenu = commandMenu;
        }

        /// <summary>
        /// Draws a given ICommanderReadable on the CharacterActionMenu by spawning button prefabs.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is creating buttons for.</param>
        /// <param name="content">The GemaObject holding the content of the CharacterActionMenu</param>
        /// <returns>The created button on the root menu.</returns>
        public abstract CommandButton Draw(ICommandReadable drawTarget, Transform content);

        /// <summary>
        /// By default, ActionDrawers don't give a specific override.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is drawing.</param>
        /// <returns></returns>
        public virtual ICommandReadable GetOverride(ICommandReadable drawTarget)
        {
            return null;
        }

        /// <summary>
        /// Spawns a new button instance from this drawer's button prefab.
        /// </summary>
        /// <param name="drawTarget">The command to draw a button for.</param>
        /// <param name="content">The transform that the spawned button should be a child of.</param>
        protected CommandButton SpawnButton(ICommandReadable drawTarget, Transform content)
        {
            // Create a Drawer for the button data we're creating a button for.
            CommandButton createdButton = GameObject.Instantiate(ButtonPrefab, content);
            createdButton.Initialize(drawTarget, CommandMenu);
            return createdButton;
        }

        /// <summary>
        /// Gets the correct ActionDrawer to use to draw this ICommanderReadable object.
        /// </summary>
        /// <param name="drawTarget">The action to get the drawer for.</param>
        /// <returns>The ActionDrawer that will draw the action.</returns>
        internal static CommandDrawer GetCommandDrawer(ICommandReadable drawTarget)
        {
            Type actionType = drawTarget.GetType();
            // Search through this assembly to find subclasses of ActionDrawer that
            // Implement the correct attribute for this actions type.
            foreach (Type type in Assembly.GetAssembly(typeof(CommandDrawer)).GetTypes())
            {
                if (!type.IsSubclassOf(typeof(CommandDrawer)) || type.IsAbstract)
                {
                    continue;
                }

                // Get the attribute storing type metadata from the ActionDrawer type
                if (Attribute.GetCustomAttribute(type, typeof(CustomCommandDrawerAttribute)) 
                    is CustomCommandDrawerAttribute cada &&
                    cada.Types.Contains(actionType))
                {
                    return Activator.CreateInstance(type) as CommandDrawer;
                }
            }
            return null;
        }
    }
}
