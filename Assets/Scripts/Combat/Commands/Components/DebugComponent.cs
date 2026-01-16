/*****************************************************************************
// File Name : Debug.cs
// Author : 
// Creation Date : 
// Last Modified : 
//
// Brief Description : 
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    [System.Serializable]
    public class DebugComponent : CommandComponent
    {
        [SerializeField] private string commandName;
        public override void ExecuteComponent(CombatEntity[] targets, CombatActor actor)
        {
            string targetString = "";
            foreach(CombatEntity target in targets)
            {
                targetString += target.name + "\n";
            }
            Debug.Log($"Command {commandName} was performed by {actor} targeting: {targetString}");
        }
    }
}
