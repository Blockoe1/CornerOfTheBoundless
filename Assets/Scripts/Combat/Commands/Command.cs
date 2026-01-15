/*****************************************************************************
// File Name : Command.cs
// Author : Eli Koederitz
// Creation Date : 12/29/2025
// Last Modified : 12/29/2025
//
// Brief Description : Data structure containing information about a given command that a combatant can perform.
*****************************************************************************/
using COTB.Combat.Characters;
using System.Collections;
using UnityEngine;

namespace COTB.Combat
{
    [CreateAssetMenu(fileName = "Command", menuName = "ScriptableObjects/Combat/Command")]
    public class Command : ScriptableObject, IButtonReadable
    {
        [SerializeField] private string commandName;
        [SerializeField, TextArea] private string commandDescription;
        [SerializeField] private Sprite icon;
        [SerializeField] private CommandTags tags;

        [SerializeReference, ClassDropdown(typeof(CommandComponent))] private CommandComponent[] commandComponents;
        [SerializeReference, ClassDropdown(typeof(CommandModifier))] private CommandModifier[] commandModifiers;

        #region Properties
        public string Name => commandName;
        public string Description => commandDescription;
        public Sprite Icon => icon;
        public CommandTags Tags => tags;

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
            foreach(CommandComponent component in commandComponents)
            {
                component.ExecuteComponent(targets, actor);
                yield return null;
            }
        }

        /// <summary>
        /// Checks if this command is valid to be used based on the current state
        /// of combat.
        /// </summary>
        /// <returns></returns>
        public bool GetDisabled()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a CharacterAction wrapping this command.
        /// </summary>
        /// <returns></returns>
        public virtual CharacterAction GetCharacterAction()
        {
            return new CommandAction(this);
        }

        #region Button Interface
        public string GetName() { return commandName; }

        public string GetDescription() { return commandDescription; }

        public Sprite GetIcon() { return icon; }

        public CommandTags GetTags() { return tags; }
        #endregion
    }
}
