/*****************************************************************************
// File Name : CombatTarget.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Allows an object to be targetable in combat.
*****************************************************************************/
using CustomAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private UnityEvent OnDeathEvent;

        #region Properties
        public int Health
        { 
            get { return health; }
            set
            {
                health = Mathf.Max(value, 0);
                if (health == 0)
                {
                    OnDeathEvent?.Invoke();
                }
            }
        }
        #endregion

        /// <summary>
        /// Deals damage to this target.
        /// </summary>
        /// <param name="damage">The amount of damage to take.</param>
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}
