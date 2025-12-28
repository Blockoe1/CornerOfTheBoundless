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
        private Vector3 targetDirection;
        private float targetSpeed;
        private float currentSpeed;
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
            moveAction.canceled += MoveAction_canceled;
        }

        private void OnDestroy()
        {
            moveAction.performed -= MoveAction_performed;
            moveAction.canceled -= MoveAction_canceled;
        }

        /// <summary>
        /// Updates the target direction for the player to move in.
        /// </summary>
        /// <param name="obj"></param>
        private void MoveAction_performed(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            targetSpeed = maxSpeed;
            // Update the target velocity that the player is moving towards.
            // Make sure the max this vector can be is the normal vector.
            targetDirection = Vector3.ClampMagnitude(new Vector3(moveInput.x, 0, moveInput.y), 1);
            if (!isMoving)
            {
                StartCoroutine(MoveRoutine());
            }
        }

        /// <summary>
        /// Reset target speed and direction when the player is no longer inputting.
        /// </summary>
        /// <param name="obj"></param>
        private void MoveAction_canceled(InputAction.CallbackContext obj)
        {
            targetSpeed = 0;
            //targetDirection = Vector3.zero;
        }

        /// <summary>
        /// Continually updates the player's destination to move them in the direction of player input.
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveRoutine()
        {
            isMoving = true;
            // Move while the player has speed or is inputting some movement.
            while (currentSpeed != 0 || targetDirection != Vector3.zero)
            {
                //Debug.Log("Accelerating");
                // Apply 1/2 of the acceleration before movement.
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime / 2);

                // Continually update the destination of the player's NavMesh agent to move in the direction of input.
                navAgent.Move(currentSpeed * Time.deltaTime * targetDirection);

                // Apply the second half after movement.  This is technically move accurate.
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime / 2);

                yield return null;
            }
            isMoving = false;
        }
    }
}
