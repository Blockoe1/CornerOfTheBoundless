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
        public override string Name => command == null ? "" : command.Name;
        public override string Description => command == null ? "" : command.Description;
        public override Sprite Icon => command == null ? null : command.Icon;
        public override CommandTags Tags => command == null ? CommandTags.None : command.Tags;

        protected override bool IsDisabled => command == null ? true : !command.CheckValid();
        #endregion
    }
}
