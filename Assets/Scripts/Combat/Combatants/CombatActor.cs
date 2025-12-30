/*****************************************************************************
// File Name : CombatActor.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Controls combatants taking actions in combat and ordering said actions by queue and slowmode.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat
{
    public class CombatActor : MonoBehaviour
    {
        [SerializeField] private CombatTarget debugTarget;
        [SerializeField] private Command debugCommand;

        /// <summary>
        /// Queue Management
        /// </summary>
        public static event Action OnQueueComplete;
        private static readonly List<CombatAction> actionQueue = new();
        private static bool isIterating;

        #region Properties
        public bool IsQueued => actionQueue.Count > 0;
        #endregion

        #region Nested
        private struct CombatAction
        {
            internal IEnumerator coroutine;
            internal CombatActor actor;
        }
        #endregion

        /// <summary>
        /// Causes this combatant to perform a given command.
        /// </summary>
        public void PerformCommand(Command toPerform, CombatTarget[] targets)
        {

        }

        #region Queue
        /// <summary>
        /// Adds a coroutine for this character to perform to the queue.
        /// </summary>
        /// <param name="coroutine">The coroutine that will be called from the queue.</param>
        public void AddToQueue(IEnumerator coroutine)
        {
            actionQueue.Add(new CombatAction() { coroutine = coroutine, actor = this });

            // Begin executing actions in the queue if we aren't already.
            if (!isIterating)
            {
                // Piggyback off of the starting actor's monobehaviour for the queue iterator.
                // Piggybacking like this will cause bugs later if the combatant is destroyed.
                StartCoroutine(ExecuteThroughQueue());
            }
        }

        /// <summary>
        /// Executes through all the actions in the action queue.
        /// </summary>
        private static IEnumerator ExecuteThroughQueue()
        {
            Coroutine currentCorotuine;

            while (actionQueue.Count > 0)
            {
                // Uses "first in, first out" rules.
                currentCorotuine = actionQueue[0].actor.StartCoroutine(actionQueue[0].coroutine);

                // Wait until the current queue item finishes.
                yield return currentCorotuine;

                actionQueue.RemoveAt(0);
                currentCorotuine = null;
            }

            isIterating = false;
            OnQueueComplete?.Invoke();
        }
        #endregion

        #region Debug
        [ContextMenu("Perform Debug Command")]
        private void PerformDebugCommand()
        {
            PerformCommand(debugCommand, new CombatTarget[] { debugTarget } );
        }
        #endregion
    }
}
