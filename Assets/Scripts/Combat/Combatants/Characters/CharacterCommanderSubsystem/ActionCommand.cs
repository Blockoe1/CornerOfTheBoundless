/*****************************************************************************
// File Name : ActionCommand.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/15/2026
//
// Brief Description : Wrapper class allowing a singular action to be selected on the CharacterCommander
// class dropdown.
*****************************************************************************/
using UnityEngine;
using COTB.Combat.Actions;

namespace COTB.Combat.Characters
{
    public class ActionCommand : CharacterCommand
    {
        [SerializeField] private CombatAction action;

        #region Properties
        public CombatAction Action => action;
        #endregion

        public ActionCommand()
        {
            this.action = null;
        }
        public ActionCommand(CombatAction action)
        {
            this.action = action;
        }

        #region Button Getters
        public override string GetName()
        {
            return action == null ? "" : action.Name;
        }

        public override string GetDescription()
        {
            return action == null ? "" : action.Description;
        }

        public override bool GetDisabled()
        {
            return action == null ? true : action.GetDisabled();
        }

        public override ActionTags GetTags()
        {
            return action == null ? ActionTags.None : action.Tags;
        }
        public override Sprite GetIcon()
        {
            return action == null ? null : action.Icon;
        }
        #endregion
    }
}
