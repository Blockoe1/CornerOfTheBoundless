/*****************************************************************************
// File Name : CharacterEntity.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Represents a player controlled character in combat.
*****************************************************************************/
using COTB.UI;
using CustomAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat
{
    public class CharacterEntity : CombatEntity
    {
        [SerializeField] private Character loadedCharacter;
        [SerializeField] private UnityEvent OnSelectEvent;
        [SerializeField] private UnityEvent OnDeselectEvent;

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
        public Command[] Skills
        {
            get
            {
                return loadedCharacter.UsableSkills;
            }
        }

        public string Name => gameObject.name;
        #endregion

        /// <summary>
        /// Controls actions that happen when this character is selected or deselected
        /// </summary>
        public void OnSelected()
        {
            Debug.Log($"Character {name} was selected");
            OnSelectEvent?.Invoke();
        }
        public void OnDeselected()
        {
            Debug.Log($"Character {name} was deselected");
            OnDeselectEvent?.Invoke();
        }

        /// <summary>
        /// Causes this character to perform an action in combat.
        /// </summary>
        /// <param name="actionData"></param>
        public void PerformAction(CombatActionData actionData)
        {
            actor.PerformCommand(actionData.Command, actionData.Targets);
        }

        public override void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public override void Heal(int healing)
        {
            throw new System.NotImplementedException();
        }
    }
}
