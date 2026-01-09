/*****************************************************************************
// File Name : CharacterEntity.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Represents a player controlled character in combat.
*****************************************************************************/
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat
{
    public class CharacterEntity : CombatEntity
    {
        [SerializeField] private Character loadedCharacter;

        #region Properties
        public ReadOnlyCollection<Command> Skills
        {
            get
            {
                return loadedCharacter.UsableSkills;
            }
        }

        public string Name => gameObject.name;
        #endregion

        public override void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public override void Heal(int healing)
        {
            throw new System.NotImplementedException();
        }
    }
}
