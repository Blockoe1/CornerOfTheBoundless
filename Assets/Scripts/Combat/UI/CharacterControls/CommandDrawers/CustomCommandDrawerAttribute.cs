/*****************************************************************************
// File Name : CustomActionDrawerAttribute.cs
// Author : Eli Koederitz
// Creation Date : 1/16/2026
// Last Modified : 1/16/2026
//
// Brief Description : Custom attribute for setting what types of commands a certain CommandDrawer can draw for.
*****************************************************************************/
using System;
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    public class CustomCommandDrawerAttribute : Attribute
    {
        public Type[] Types { get; }

        public CustomCommandDrawerAttribute(params Type[] type)
        {
            Types = type;
        }
    }
}
