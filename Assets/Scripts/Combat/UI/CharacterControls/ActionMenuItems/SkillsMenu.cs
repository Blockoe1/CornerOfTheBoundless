/*****************************************************************************
// File Name : SkillsMenu.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2026
// Last Modified : 1/4/2026
//
// Brief Description : Creates a skills menu for this character on the action menu.
*****************************************************************************/
using COTB.UI;
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

        #region Nested
        /// <summary>
        /// Wrapper class that reroutes 
        /// </summary>
        private class CommandButton : IButtonReadable
        {
            private readonly Command cmd;

            internal CommandButton(Command cmd)
            {
                this.cmd = cmd;
            }

            /// <summary>
            /// Information getters reroute to the wrapped command.
            /// </summary>
            public string GetDescription()
            {
                return cmd.Description;
            }
            public string GetName()
            {
                return cmd.Name;
            }
            public Sprite GetIcon()
            {
                return cmd.Icon;
            }

            /// <summary>
            /// For now, commands are always in the enabled state until I add systems to disable them.
            /// </summary>
            /// <returns></returns>
            public ButtonState CheckCurrentState()
            {
                return ButtonState.Default; 
            }

            /// <summary>
            /// When a command button is clicked, start targeting for that command.
            /// </summary>
            /// <exception cref="System.NotImplementedException"></exception>
            public void OnButtonClicked()
            {
                throw new System.NotImplementedException();
            }
        }
        #endregion

        /// <summary>
        /// Reads this character's skill information and creates button readables from those skills.
        /// </summary>
        /// <returns>An array of CommandButtons that link to this character's skills.</returns>
        protected override IButtonReadable[] GetButtonData()
        {
            IButtonReadable[] buttons = new IButtonReadable[character.Skills.Length];
            for (int i = 0; i < character.Skills.Length; i++)
            {
                buttons[i] = new CommandButton(character.Skills[i]);
            }
            return buttons;
        }
    }
}
