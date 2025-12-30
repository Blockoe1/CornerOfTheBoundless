/*****************************************************************************
// File Name : DamageHeal.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Modifies a command so that the user heals when the command deals damage.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    [System.Serializable]
    public class DamageHeal : CommandModifier
    {
        [SerializeField] private float healingRatio;
    }
}
