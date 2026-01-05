/*****************************************************************************
// File Name : CharacterController.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2026
// Last Modified : 1/4/2026
//
// Brief Description : Controlls player interactions with this character.
*****************************************************************************/
using COTB.UI;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat.UI.CharacterControls
{
    public class CharacterCommander : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnSelectEvent;
        [SerializeField] private UnityEvent OnDeselectEvent;

        private ActionMenuItem[] menuItems;

        private bool hasInitialized;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private CombatActor actor;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            actor = GetComponent<CombatActor>();
        }
        #endregion

        #region Properties
        public bool HasInitialized => hasInitialized;
        #endregion

        /// <summary>
        /// Find all menu items on awake.
        /// </summary>
        private void Awake()
        {
            // Gets all of the ActionMenuItem components on this character in reverse ButtonIndex order.
            menuItems = GetComponents<ActionMenuItem>().OrderBy(item => item.ButtonIndex).Reverse().ToArray();
        }

        /// <summary>
        /// Causes this character to perform an action in combat.
        /// </summary>
        /// <param name="actionData"></param>
        public void PerformAction(CombatActionData actionData)
        {
            actor.PerformCommand(actionData.Command, actionData.Targets);
        }

        /// <summary>
        /// Initializes this character
        /// </summary>
        /// <param name="actionMenu"></param>
        public void Initialize(CharacterActionMenu actionMenu)
        {
            foreach (var item in menuItems)
            {
                item.Initialize(actionMenu);
            }
            hasInitialized = true;
        }

        /// <summary>
        /// Controls what happens when this character is selected.
        /// </summary>
        public void OnSelect()
        {
            OnSelectEvent?.Invoke();
            foreach(var item in menuItems)
            {
                item.OnSelected();
            }
        }

        /// <summary>
        /// Controls what happens when this character is deselected.
        /// </summary>
        public void OnDeselect()
        {
            OnDeselectEvent?.Invoke();
            foreach(var item in menuItems)
            {
                item.OnDeselected();
            }
        }
    }
}
