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
using CustomAttributes;
using UnityEngine.InputSystem;
using System.Collections;

namespace COTB.Overworld
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterMovement : MonoBehaviour
    {
        #region CONSTS
        private const string MOVE_ACTION_NAME = "Move";
        #endregion

        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;

        /// <summary>
        /// Input
        /// </summary>
        private InputAction moveAction;

        /// <summary>
        /// Movement
        /// </summary>
        private Vector3 targetVelocity;
        private Vector3 currentVelocity;
        private bool isMoving;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] protected NavMeshAgent navAgent;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        #endregion

        /// <summary>
        /// Setup input references.
        /// </summary>
        private void Awake()
        {
            moveAction = InputSystem.actions.FindAction(MOVE_ACTION_NAME);
            moveAction.performed += MoveAction_performed;
        }
        private void OnDestroy()
        {
            moveAction.performed -= MoveAction_performed;
        }

        /// <summary>
        /// Updates the target direction for the player to move in.
        /// </summary>
        /// <param name="obj"></param>
        private void MoveAction_performed(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            // Update the target velocity that the player is moving towards.
            targetVelocity = new Vector3(moveInput.x, 0, moveInput.y) * maxSpeed;
            if (!isMoving)
            {
                StartCoroutine(MoveRoutine());
            }
        }

        /// <summary>
        /// Continually updates the player's destination to move them in the direction of player input.
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveRoutine()
        {
            isMoving = true;
            while (currentVelocity != Vector3.zero || targetVelocity != Vector3.zero)
            {
                Debug.Log("Accelerating");
                // Apply 1/2 of the acceleration before movement.
                currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime / 2);

                // Continually update the destination of the player's NavMesh agent to move in the direction of input.
                navAgent.Move(currentVelocity * Time.deltaTime);

                // Apply the second half after movement.  This is technically move accurate.
                currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime / 2);

                yield return null;
            }
            isMoving = false;
        }
    }
}
