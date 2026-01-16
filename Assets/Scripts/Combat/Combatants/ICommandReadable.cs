/*****************************************************************************
// File Name : ICommandReadable.cs
// Author : Eli Koederitz 
// Creation Date : 1/15/2025
// Last Modified : 1/15/2025
//
// Brief Description : Interface for a button gettind display data from a combat action.
// Remarks: ONLY for reading data.  Not used for checking state or handling any character
// specific context.
*****************************************************************************/

using UnityEngine;
using COTB.Combat.Actions;

namespace COTB.Combat
{
    public interface ICommandReadable
    {
        string GetName();
        string GetDescription();
        Sprite GetIcon();
        ActionTags GetTags();
        bool GetDisabled();
    }
}
