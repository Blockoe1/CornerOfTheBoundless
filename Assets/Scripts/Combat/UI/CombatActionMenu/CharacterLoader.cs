/*****************************************************************************
// File Name : CharacterLoader.cs
// Author : Eli Koederitz
// Creation Date : 1/3/2025
// Last Modified : 1/3/2025
//
// Brief Description : Manages loading character data to the action menu.
*****************************************************************************/
using CustomAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace COTB.Combat.UI
{
    public class CharacterLoader : MonoBehaviour
    {
        [SerializeField] private Button skillsButton;

        private readonly Dictionary<CharacterCombatant, CombatSubMenu> characterMenuDict = new();

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CharacterActionMenu characterActionMenu;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            characterActionMenu = GetComponent<CharacterActionMenu>();
        }
        #endregion

        /// <summary>
        /// Loads a given character's data to the action menu.
        /// </summary>
        public void LoadCharacter(CharacterCombatant toLoad)
        {
            //  If the character doesn't have a registered skills menu, create one.
            if (!characterMenuDict.ContainsKey(toLoad))
            {
                // conver the character's skills into an array of ButtonReadable wrapper classes.
                IButtonReadable[] skills = new IButtonReadable[toLoad.Skills.Length];
                for(int i = 0; i < skills.Length; i++)
                {
                    skills[i] = new SkillButtonData(toLoad.Skills[i]);
                }

                // If this character has no valid skills, skip creating a sub menu.
                if (skills.Length > 0)
                {
                    characterMenuDict.Add(toLoad, characterActionMenu.CreateSubMenu(skills, toLoad.name + "SkillsMenu", skillsButton));
                }
            }

            // If there is still no skills menu after attempting to create one, then this character has no skills and
            // the skills menu cannot be opened.
            if (characterMenuDict.ContainsKey(toLoad))
            {
                characterActionMenu.CurrentSkillsMenu = characterMenuDict[toLoad];
            }
        }
    }
}
