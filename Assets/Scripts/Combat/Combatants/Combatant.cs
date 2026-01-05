/*****************************************************************************
// File Name : Combatant.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Abstract class for controller classes that define a type of entity that can be targeted
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    public abstract class Combatant : MonoBehaviour
    {
        public abstract void TakeDamage(int damage);
    }
}
