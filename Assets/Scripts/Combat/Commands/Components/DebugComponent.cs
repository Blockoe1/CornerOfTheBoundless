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
        public override void ExecuteComponent(CombatEntity[] targets, CombatActor actor)
        {
            string 
            Debug.Log($"Command was performed by {actor} at {targets} targets.");
        }
    }
}
