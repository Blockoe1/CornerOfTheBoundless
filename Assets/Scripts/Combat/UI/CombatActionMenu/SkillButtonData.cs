/*****************************************************************************
// File Name : SkillButtonData.cs
// Author : Eli Koederitz
// Creation Date : 1/3/2026
// Last Modified : 1/3/2026
//
// Brief Description : Wrapper class that links a button to using a skill of a given combatant.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.UI
{
    public class SkillButtonData : IButtonReadable
    {
        private Command cmd;

        public SkillButtonData(Command cmd)
        {
            this.cmd = cmd;
        }

        /// <summary>
        /// Checks if this skill can be used by the character.
        /// </summary>
        /// <returns></returns>
        public ButtonState CheckCurrentState()
        {
            // Just return default for now since I don't know how I'm going to check if the command is allowed or not.
            return ButtonState.Default;
        }

        /// <summary>
        /// Info Getters
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return cmd.Description;
        }
        public string GetName()
        {
            return cmd.Name;
        }

        /// <summary>
        /// When a skill button is clicked, start targeting for it.
        /// </summary>
        public void OnButtonClicked()
        {
            throw new System.NotImplementedException();
        }
    }
}
