/*****************************************************************************
// File Name : Character.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2025
// Last Modified : 1/4/2025
//
// Brief Description : Data structure that describes a character and their current state.
*****************************************************************************/
using System.Collections.ObjectModel;
using System;
using UnityEngine;

namespace COTB.Combat
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField, Range(1, 99)] private byte level;
        [Header("Combat Settings")]
        [SerializeField] private Command[] skills;

        #region Properties
        public ReadOnlyCollection<Command> UsableSkills
        { 
            get
            {
                // Add modifiers that allow excluding skills based on what's not unlocked.
                return Array.AsReadOnly(skills);
            }
        }
        #endregion
    }
}
