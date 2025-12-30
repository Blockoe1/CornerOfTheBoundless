/*****************************************************************************
// File Name : CombatQueue.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Manages the order that combat actors perform actions.  Must be it's own object so it doesn't
// cause problems when a combatant is destroyed.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COTB.Combat
{
    public class CombatQueue : MonoBehaviour
    {
        private static CombatQueue queueCoroutineObj;
        public static event Action OnQueueComplete;
        private static readonly Queue<CombatAction> actionQueue = new();
        private static bool isIterating;

        #region Properties
        public bool IsQueued => actionQueue.Count > 0;
        private static CombatQueue queueCoroutineObject
        {
            get
            {
                // If no GO exists to hold the coroutine for the queue, create one.
                if (queueCoroutineObj == null)
                {
                    GameObject go = new GameObject("CombatQueue");
                    queueCoroutineObj = go.AddComponent<CombatQueue>();
                }
                return queueCoroutineObj;
            }
        }

        #endregion

        #region Nested
        private struct CombatAction
        {
            internal IEnumerator coroutine;
            internal CombatActor actor;
        }
        #endregion

        #region Queue
        /// <summary>
        /// Adds a coroutine for this character to perform to the queue.
        /// </summary>
        /// <param name="coroutine">The coroutine that will be called from the queue.</param>
        public static void AddToQueue(IEnumerator coroutine, CombatActor actor)
        {
            actionQueue.Enqueue(new CombatAction() { coroutine = coroutine, actor = actor });

            // Begin executing actions in the queue if we aren't already.
            if (!isIterating)
            {
                // Piggyback off of the starting actor's monobehaviour for the queue iterator.
                // Piggybacking like this will cause bugs later if the combatant is destroyed.
                queueCoroutineObject.StartCoroutine(ExecuteThroughQueue());
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
                CombatAction action = actionQueue.Dequeue();
                currentCorotuine = action.actor.StartCoroutine(action.coroutine);

                // Wait until the current queue item finishes.
                yield return currentCorotuine;
                currentCorotuine = null;
            }

            isIterating = false;
            OnQueueComplete?.Invoke();
        }
        #endregion
    }
}
