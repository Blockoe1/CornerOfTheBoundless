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
        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CharacterEntity character;
        internal override void Reset(CharacterCommander ownedCommander)
        {
            character = ownedCommander.GetComponent<CharacterEntity>();
        }
        #endregion
    }
}
