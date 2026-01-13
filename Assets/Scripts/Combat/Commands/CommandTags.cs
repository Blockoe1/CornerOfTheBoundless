/*****************************************************************************
// File Name : CommandTags.cs
// Author : Eli Koederitz
// Creation Date : 1/12/2026
// Last Modified : 1/12/2026
//
// Brief Description : Flags enum that can be applied to categorize certain types of commands and is used for 
// locking commands.
*****************************************************************************/
using System;

namespace COTB.Combat
{
    [Flags]
    public enum CommandTags
    {
        None = 0,
        Attack = 1 << 0,
        Skill = 1 << 1,
        Item = 1 << 2,
        Lurk = 1 << 3,
        Flee = 1 << 4
    }
}
