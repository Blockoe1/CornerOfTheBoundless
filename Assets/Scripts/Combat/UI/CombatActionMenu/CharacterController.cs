/*****************************************************************************
// File Name : CharacterController.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2026
// Last Modified : 1/4/2026
//
// Brief Description : Controlls loading this character's data to the action menu.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.UI
{
    public class CharacterController : MonoBehaviour
    {
        private ActionMenuItem[] menuItems;

        /// <summary>
        /// Find all menu items on awake.
        /// </summary>
        private void Awake()
        {
            menuItems = GetComponents<ActionMenuItem>();
        }


    }
}
