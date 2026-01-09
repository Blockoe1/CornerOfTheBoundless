/*****************************************************************************
// File Name : Monster.cs
// Author : Eli Koederitz
// Creation Date : 1/9/2025
// Last Modified : 1/9/2025
//
// Brief Description : Combat entity that controls targetable monsters.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    public class MonsterEntity : CombatEntity
    {
        public override void Heal(int healing)
        {
            throw new System.NotImplementedException();
        }

        public override void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
