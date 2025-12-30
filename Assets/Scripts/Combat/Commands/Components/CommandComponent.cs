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

namespace COTB.Combat
{
    [System.Serializable]
    public abstract class CommandComponent
    {
        public abstract void ExecuteComponent(CombatTarget[] targets, CombatActor actor);
    }
}
