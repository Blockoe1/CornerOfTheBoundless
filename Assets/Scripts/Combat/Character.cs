/*****************************************************************************
// File Name : Character.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2025
// Last Modified : 1/4/2025
//
// Brief Description : Data structure that describes a character and their current state.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private string characterName;
        [Header("Combat Settings")]
        [SerializeField] private Command[] skills;

        #region Properties
        public Command[] UsableSkills
        { 
            get
            {
                return skills;
            }
        }
        #endregion
    }
}
