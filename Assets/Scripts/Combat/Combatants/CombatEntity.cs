/*****************************************************************************
// File Name : CombatEntity.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Abstract class for controller classes that define a type of entity that can be targeted
*****************************************************************************/
using CustomAttributes;
using UnityEngine;

namespace COTB.Combat
{
    public abstract class CombatEntity : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int health;

        /// <summary>
        /// Causes this combatant to take damage.
        /// </summary>
        /// <param name="damage"></param>
        public abstract void TakeDamage(int damage);

        /// <summary>
        /// Restores health to this combatant.
        /// </summary>
        /// <param name="healing"></param>

        public abstract void Heal(int healing);

        // TODO: Apply Effect
    }
}
