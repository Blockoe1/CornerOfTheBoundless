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

        /// <summary>
        /// Controls the main logic of the command.
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        public IEnumerator CommandMain(CombatTarget[] targets,  CombatActor actor)
        {
            yield return null;
        }
    }
}
