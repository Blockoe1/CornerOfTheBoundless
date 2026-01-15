/*****************************************************************************
// File Name : CommandAction.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/12/2026
//
// Brief Description : Character action that represents using a singular command.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.Characters
{
    public class CommandAction : CharacterAction
    {
        [SerializeField] private Command command;

        #region Properties
        public Command Command => command;
        #endregion

        public CommandAction(Command command)
        {
            this.command = command;
        }

        #region Button Getters
        public override string GetName()
        {
            return command == null ? "" : command.Name;
        }

        public override string GetDescription()
        {
            return command == null ? "" : command.Description;
        }

        public override bool GetDisabled()
        {
            return command == null ? true : command.GetDisabled();
        }

        public override CommandTags GetTags()
        {
            return command == null ? CommandTags.None : command.Tags;
        }
        public override Sprite GetIcon()
        {
            return command == null ? null : command.Icon;
        }
        #endregion
    }
}
