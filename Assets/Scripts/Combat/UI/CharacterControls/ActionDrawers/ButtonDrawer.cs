/*****************************************************************************
// File Name : ButtonDrawer.cs
// Author : Eli Koederitz
// Creation Date : 1/16/2026
// Last Modified : 1/16/2026
//
// Brief Description : Spawns a button prefab for the given drawTarget.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    [CustomActionDrawer(typeof(Command))]
    public class ButtonDrawer : ActionDrawer
    {
        /// <summary>
        /// Spawns a button prefab instance for the drawTarget.
        /// </summary>
        /// <param name="drawTarget">The command being drawn.</param>
        /// <param name="content">The transform that the spawned button is a child of.</param>
        /// <param name="actionMenu">The action menu this button belongs to.</param>
        /// <returns>The created CharacterButton</returns>
        public override CharacterButton Draw(ICommanderReadable drawTarget, Transform content)
        {
            CharacterButton spawnedButton = SpawnButton(drawTarget, content);
            // Hook up the button so it sends command info to the character menu when pressed.
            spawnedButton.AddEnabledListener((command) => ActionMenu.OnCommandSelected(command));
            return spawnedButton;
        }
    }
}
