/*****************************************************************************
// File Name : SingletonButtonDrawer.cs
// Author : Eli Koederitz
// Creation Date : 1/15/2026
// Last Modified : 1/15/2026
//
// Brief Description : ActionDrawer for single command actions that have a shared pre-created
// button on the ActionMenu for efficiency.
*****************************************************************************/
using COTB.Combat.Characters;
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    [CustomActionDrawer(typeof(Characters.CommandAction))]
    public class SingletonButtonDrawer : ActionDrawer
    {
        /// <summary>
        /// Finds a reference to a CharacterButton that shares tags with the given draw target.
        /// </summary>
        /// <param name="drawTarget">The action that this Drawer is creating a button for.</param>
        /// <param name="content">The parent GameObject of buttons to create for the root ActionMenu.</param>
        /// <param name="subMenuPrefab">The prefab for creating a sub menu.</param>
        /// <param name="buttonPrefab">The prefab for creating a button.</param>
        /// <param name="actionMenu">The ActionMenu that is this drawer applies to.</param>
        /// <returns>The created button on the root ActionMenu.</returns>
        public override CharacterButton Draw(ICommanderReadable drawTarget, Transform content, 
            CombatSubMenu subMenuPrefab, CharacterButton buttonPrefab, CharacterActionMenu actionMenu)
        {
            // Find the button whose tags match the drawTargets.
            foreach(CharacterButton button in content.GetComponentsInChildren<CharacterButton>())
            {
                if ((button.Tags & drawTarget.GetTags()) == button.Tags)
                {
                    return button;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the drawn action as an override since when drawing, all SingletonButtonDrawers
        /// point to the same buttons.
        /// </summary>
        public override ICommanderReadable GetOverride(ICommanderReadable drawTarget)
        {
            return drawTarget;
        }
    }
}
