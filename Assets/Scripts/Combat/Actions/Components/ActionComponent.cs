/*****************************************************************************
// File Name : CommandComponent.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Specifies a component that makes up part of a given command.
*****************************************************************************/
using System.Collections;
using UnityEngine;

namespace COTB.Combat.Actions
{
    [System.Serializable]
    public abstract class ActionComponent
    {
        public abstract void ExecuteComponent(CombatEntity[] targets, CombatActor actor);
    }
}
