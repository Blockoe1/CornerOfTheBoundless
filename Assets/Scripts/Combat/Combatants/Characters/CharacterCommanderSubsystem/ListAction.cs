/*****************************************************************************
// File Name : ListAction.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/12/2026
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
        [SerializeField] protected CommandTags actionTags;

        #region Properties
        public override string Name => actionName;
        public override string Description => actionDescription;
        public override Sprite Icon => actionIcon;
        public override CommandTags Tags => actionTags;
        // Sub-Menus can never be disabled, only locked for simplicity.
        protected override bool IsDisabled => false;
        #endregion
    }
}
