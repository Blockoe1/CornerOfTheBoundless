/*****************************************************************************
// File Name : SkillsMenu.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2026
// Last Modified : 1/9/2026
//
// Brief Description : Creates a skills menu for this character on the action menu.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.UI.CharacterControls
{
    [RequireComponent(typeof(CharacterCommander))]
    public class SkillsMenu : SubMenuItem
    {
        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CharacterEntity character;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            character = GetComponent<CharacterEntity>();
        }
        #endregion

        /// <summary>
        /// Reads this character's skill information and creates button readables from those skills.
        /// </summary>
        /// <returns>An array of CommandButtons that link to this character's skills.</returns>
        protected override IButtonReadable[] GetButtonData()
        {
            IButtonReadable[] buttons = new IButtonReadable[character.Skills.Count];
            for (int i = 0; i < character.Skills.Count; i++)
            {
                buttons[i] = new CommandButton(character.Skills[i], rootMenu, UseSkill);
            }
            return buttons;
        }

        /// <summary>
        /// Function used as a delgate for CombatButtons to tell the character to use a skill.
        /// </summary>
        /// <param name="action">Wrapper class for the action to perform.</param>
        private void UseSkill(CombatActionData action)
        {
            commander.PerformAction(action);
        }
    }
}
