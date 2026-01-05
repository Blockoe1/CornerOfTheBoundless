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

namespace COTB.Combat.UI
{
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
        /// 
        /// </summary>
        private class CommandButton : IButtonReadable
        {
            public ButtonState CheckCurrentState()
            {
                throw new System.NotImplementedException();
            }

            public string GetDescription()
            {
                throw new System.NotImplementedException();
            }

            public string GetName()
            {
                throw new System.NotImplementedException();
            }

            public Sprite GetIcon()
            {

            }

            public int GetCost()
            {

            }

            public void OnButtonClicked()
            {
                throw new System.NotImplementedException();
            }
        }
        #endregion

        /// <summary>
        /// Reads this character's skill information and creates button readables from those skills.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override IButtonReadable[] GetButtonData()
        {
            IButtonReadable[] buttons = new IButtonReadable[character.Skills.Length];
            for (int i = 0; i < character.Skills.Length; i++)
            {

            }
        }
    }
}
