/*****************************************************************************
// File Name : CombatCommander.cs
// Author : Eli Koederitz
// Creation Date : 1/11/2026
// Last Modified : 1/11/2026
//
// Brief Description : Abstract base class for components that control how a certain combatant type determines which
// action to perform.
*****************************************************************************/
using UnityEditor;
using UnityEngine;

namespace COTB.Combat
{
    public abstract class CombatCommander : MonoBehaviour
    {
        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CombatActor actor;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        protected virtual void Reset()
        {
            actor = GetComponent<CombatActor>();
        }
        #endregion

        #region Properties
        protected CombatActor Actor => actor;
        #endregion
    }
}
