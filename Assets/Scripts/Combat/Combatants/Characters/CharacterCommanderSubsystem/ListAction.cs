/*****************************************************************************
// File Name : ListAction.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/15/2026
//
// Brief Description : Represents an action comprising of multiple sub-actions.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.Characters
{
    public abstract class ListAction : CharacterAction
    {
        [SerializeField] protected string actionName;
        [SerializeField] protected Sprite actionIcon;
        [SerializeField, TextArea] protected string actionDescription;
        [SerializeField] protected ActionTags actionTags;

        #region Properties
        public abstract ICommanderReadable[] SubActions { get; }
        #endregion

        #region Button Getters
        public override string GetName() { return actionName; }
        public override string GetDescription() { return actionDescription; }
        public override Sprite GetIcon() { return actionIcon; }
        public override ActionTags GetTags() { return actionTags; }
        #endregion
    }
}
