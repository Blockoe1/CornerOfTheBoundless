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

namespace COTB.Combat.UI.CharacterMenu
{
    public abstract class ActionDrawer
    {
        /// <summary>
        /// Draws a given ICommanderReadable on the CharacterActionMenu by spawning button prefabs.
        /// </summary>
        /// <param name="drawTarget">The ICommanderReadable that this drawer is creating buttons for.</param>
        /// <param name="content">The GemaObject holding the content of the CharacterActionMenu</param>
        /// <param name="subMenuPrefab">The prefab to use for creating a Sub-Menu</param>
        /// <param name="buttonPrefab">The prefab to use for creating buttons.</param>
        /// <param name="actionMenu">The menu that this drawer is drawing on.</param>
        /// <returns>The created button on the root menu.</returns>
        public abstract CharacterButton Draw(ICommanderReadable drawTarget, Transform content, 
            CombatSubMenu subMenuPrefab, CharacterButton buttonPrefab, CharacterActionMenu actionMenu);

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
        /// Creates a button instance loaded with the given buttonData.
        /// </summary>
        /// <param name="buttonData">The button data to construct the button from.</param>
        /// <param name="actionMenu">The root ActionMenu this button belongs to.</param>
        /// <param name="buttonPrefab">The prefab to use when spawning the button.</param>
        /// <returns>The created button.</returns>
        protected static CharacterButton CreateButton(ICommanderReadable buttonData, CharacterActionMenu actionMenu, 
            CharacterButton buttonPrefab)
        {
            CharacterButton createdButton = GameObject.Instantiate(buttonPrefab, actionMenu.Content);
            createdButton.Initialize(buttonData, actionMenu);
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
