/*****************************************************************************
// File Name : SkillsListAction.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/12/2026
//
// Brief Description : character action that loads all of the saved skills this character can use.
*****************************************************************************/
using System.Linq;
using UnityEngine;

namespace COTB.Combat.Characters
{
    public class SkillsListAction : ListAction
    {
        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CharacterEntity character;

        internal override void GetComponents(CharacterCommander ownedCommander)
        {
            character = ownedCommander.GetComponent<CharacterEntity>();
        }
        #endregion

        #region Properties
        public override ICommanderReadable[] SubActions
        {
            get
            {
                return character.Skills.ToArray();
            }
        }
        #endregion

        /// <summary>
        /// ListActions cannot be disabled.
        /// </summary>
        public override bool GetDisabled() { return false; }
    }
}
