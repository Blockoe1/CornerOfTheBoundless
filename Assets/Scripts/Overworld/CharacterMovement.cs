/*****************************************************************************
// File Name : CharacterMovement.cs
// Author : Brandon Koederitz
// Creation Date : 12/20/2025
// Last Modified : 12/20/2025
//
// Brief Description : Controls character movement in the overworld.
*****************************************************************************/
using UnityEngine;
using UnityEngine.AI;

namespace COTB.Overworld
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterMovement : MonoBehaviour
    {
        #region Component References
        [Header("Components")]
        [SerializeReference] protected NavMeshAgent navAgent;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        #endregion
    }
}
