/*****************************************************************************
// File Name : CustomActionDrawer.cs
// Author : 
// Creation Date : 
// Last Modified : 
//
// Brief Description : 
*****************************************************************************/
using System;
using UnityEngine;

namespace COTB.Combat.UI.CharacterMenu
{
    public class CustomActionDrawerAttribute : Attribute
    {
        public Type[] Types { get; }

        public CustomActionDrawerAttribute(params Type[] type)
        {
            Types = type;
        }
    }
}
