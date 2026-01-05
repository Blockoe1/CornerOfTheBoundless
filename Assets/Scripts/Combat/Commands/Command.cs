/*****************************************************************************
// File Name : Command.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Data structure containing information about a given command that a combatant can perform.
*****************************************************************************/
using System.Collections;
using UnityEngine;

namespace COTB.Combat
{
    [CreateAssetMenu(fileName = "Command", menuName = "ScriptableObjects/Combat/Command")]
    public class Command : ScriptableObject
    {
        [SerializeField] private string commandName;
        [SerializeField, TextArea] private string commandDescription;

        [SerializeReference, ClassDropdown(typeof(CommandComponent))] private CommandComponent[] commandComponents;
        [SerializeReference, ClassDropdown(typeof(CommandModifier))] private CommandModifier[] commandModifiers;

        #region Properties
        public string Name => commandName;
        public string Description => commandDescription;

        #endregion

        /// <summary>
        /// Controls the main logic of the command.
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        public IEnumerator CommandMain(Combatant[] targets,  CombatActor actor)
        {
            // Loop through each component and apply it's effects.
            foreach(CommandComponent component in commandComponents)
            {
                component.ExecuteComponent(targets, actor);
                yield return null;
            }
        }
    }
}
