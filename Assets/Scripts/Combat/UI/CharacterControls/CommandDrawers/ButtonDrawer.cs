/*****************************************************************************
// File Name : ButtonDrawer.cs
// Author : Eli Koederitz
// Creation Date : 1/16/2026
// Last Modified : 1/16/2026
//
// Brief Description : Spawns a button prefab for the given drawTarget.
*****************************************************************************/
using UnityEngine;
using COTB.Combat.Actions;

namespace COTB.Combat.UI.CharacterMenu
{
    [CustomCommandDrawer(typeof(CombatAction))]
    public class ButtonDrawer : CommandDrawer
    {
        /// <summary>
        /// Spawns a button prefab instance for the drawTarget.
        /// </summary>
        /// <param name="drawTarget">The command being drawn.</param>
        /// <param name="content">The transform that the spawned button is a child of.</param>
        /// <returns>The created CharacterButton</returns>
        public override CommandButton Draw(ICommandReadable drawTarget, Transform content)
        {
            CommandButton spawnedButton = SpawnButton(drawTarget, content);
            // Hook up the button so it sends command info to the character menu when pressed.
            spawnedButton.AddEnabledListener((action) => CommandMenu.OnActionSelected(action));
            return spawnedButton;
        }
    }
}
