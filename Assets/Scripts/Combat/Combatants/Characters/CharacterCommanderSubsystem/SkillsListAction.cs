/*****************************************************************************
// File Name : SkillsListAction.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/12/2026
//
// Brief Description : character action that loads all of the saved skills this character can use.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.Characters
{
    public class SkillsListAction : ListAction
    {
        private CharacterAction[] subActions;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CharacterEntity character;

        internal override void GetComponents(CharacterCommander ownedCommander)
        {
            character = ownedCommander.GetComponent<CharacterEntity>();
        }
        #endregion

        #region Properties
        public override CharacterAction[] SubActions
        {
            get
            {
                if (subActions == null)
                {
                    subActions = new CharacterAction[character.Skills.Count];
                    for(int i = 0; i < subActions.Length; i++)
                    {
                        subActions[i] = character.Skills[i].GetCharacterAction();
                    }
                }
                return subActions;
            }
        }
        #endregion

        /// <summary>
        /// ListActions cannot be disabled.
        /// </summary>
        public override bool GetDisabled() { return false; }
    }
}
