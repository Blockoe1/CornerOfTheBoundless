/*****************************************************************************
// File Name : CommandState.cs
// Author : Eli Koederitz
// Creation Date : 1/3/2025
// Last Modified : 1/3/2025
//
// Brief Description : Represents the different states that commands issued to the combat actor can be in.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat
{
    public enum CommandState
    {
        Enabled,
        Disabled,
        Locked
    }
}
