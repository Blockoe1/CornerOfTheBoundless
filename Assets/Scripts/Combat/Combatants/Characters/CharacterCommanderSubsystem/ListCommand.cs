/*****************************************************************************
// File Name : ListCommand.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/15/2026
//
// Brief Description : Represents a command comprising of multiple sub-commands.
*****************************************************************************/
using UnityEngine;
using COTB.Combat.Actions;

namespace COTB.Combat.Characters
{
    public abstract class ListCommand : CharacterCommand
    {
        [SerializeField] protected string commandName;
        [SerializeField] protected Sprite commandIcon;
        [SerializeField, TextArea] protected string commandDescription;
        [SerializeField] protected ActionTags commandTags;

        #region Properties
        public abstract ICommandReadable[] SubCommands { get; }
        #endregion

        #region Button Getters
        public override string GetName() { return commandName; }
        public override string GetDescription() { return commandDescription; }
        public override Sprite GetIcon() { return commandIcon; }
        public override ActionTags GetTags() { return commandTags; }
        #endregion
    }
}
