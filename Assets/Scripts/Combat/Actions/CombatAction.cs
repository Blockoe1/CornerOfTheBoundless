/*****************************************************************************
// File Name : Command.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 1/15/2026
//
// Brief Description : Data structure containing information about a given command that a combatant can perform.
*****************************************************************************/
using System.Collections;
using UnityEngine;

namespace COTB.Combat.Actions
{
    [CreateAssetMenu(fileName = "NewAction", menuName = "ScriptableObjects/Combat/Action")]
    public class CombatAction : ScriptableObject, ICommandReadable
    {
        [SerializeField] private string actionName;
        [SerializeField, TextArea] private string actionDescription;
        [SerializeField] private Sprite icon;
        [SerializeField] private ActionTags tags;

        [SerializeReference, ClassDropdown(typeof(ActionComponent))] private ActionComponent[] actionComponents;
        [SerializeReference, ClassDropdown(typeof(ActionModifier))] private ActionModifier[] actionModifiers;

        #region Properties
        public string Name => actionName;
        public string Description => actionDescription;
        public Sprite Icon => icon;
        public ActionTags Tags => tags;

        #endregion

        /// <summary>
        /// Controls the main logic of the command.
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        public IEnumerator CommandMain(CombatEntity[] targets,  CombatActor actor)
        {
            // Loop through each component and apply it's effects.
            foreach(ActionComponent component in actionComponents)
            {
                component.ExecuteComponent(targets, actor);
                yield return null;
            }
        }

        /// <summary>
        /// Checks if this action is valid to be used based on the current state
        /// of combat.
        /// </summary>
        /// <returns></returns>
        public bool GetDisabled()
        {
            return false;
        }

        #region Command Interface
        public string GetName() { return actionName; }

        public string GetDescription() { return actionDescription; }

        public Sprite GetIcon() { return icon; }

        public ActionTags GetTags() { return tags; }
        #endregion
    }
}
