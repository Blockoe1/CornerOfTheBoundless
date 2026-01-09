/*****************************************************************************
// File Name : CommandButton.cs
// Author : Eli Koederitz
// Creation Date : 1/9/2025
// Last Modified : 1/9/2025
//
// Brief Description : Wrapper class containing information about the player telling the character to use a command.
*****************************************************************************/
using COTB.UI;
using UnityEngine;

namespace COTB.Combat.UI.CharacterControls
{
    public delegate void CommandPerformFunction(CombatActionData action);
    public class CommandButton : IButtonReadable
    {
        private readonly Command cmd;
        private readonly RootMenu rootMenu;
        private readonly CommandPerformFunction performFunction;

        internal CommandButton(Command cmd, RootMenu rootMenu, CommandPerformFunction performFunction)
        {
            this.cmd = cmd;
            this.rootMenu = rootMenu;
            this.performFunction = performFunction;
        }

        /// <summary>
        /// Information getters reroute to the wrapped command.
        /// </summary>
        public string GetDescription()
        {
            return cmd.Description;
        }
        public string GetName()
        {
            return cmd.Name;
        }
        public Sprite GetIcon()
        {
            return cmd.Icon;
        }

        /// <summary>
        /// For now, commands are always in the enabled state until I add systems to disable them.
        /// </summary>
        /// <returns></returns>
        public ButtonState CheckCurrentState()
        {
            return ButtonState.Enabled;
        }

        /// <summary>
        /// When a command button is clicked, start targeting for that command.
        /// </summary>
        public void OnButtonClicked()
        {
            // Pass in null targets for now so I can test the system.  Later, it will use a targeting system.
            // TODO: Implement targeting systems.
            performFunction(new CombatActionData(cmd, null));
        }
    }
}
