/*****************************************************************************
// File Name : CharacterActionMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Player interface with the combat system that allows them to issue commands to characters.
*****************************************************************************/
using COTB.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace COTB.Combat.UI
{
    [RequireComponent(typeof(RootMenu))]
    public class CharacterActionMenu : MonoBehaviour
    {
        [SerializeField] private ScrollWithSelected scrollController;
        [SerializeField] private CombatSubMenu subMenuPrefab;
        [SerializeField] private CombatButton combatButtonPrefab;
        [SerializeField] private UnityEvent<CombatActionData> OnActionSelected;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private RootMenu rootMenu;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            rootMenu = GetComponent<RootMenu>();
        }
        #endregion

        #region Properties
        public Transform Content => rootMenu.Content;
        public ScrollWithSelected ScrollController => scrollController;
        public RootMenu RootMenu => rootMenu;
        #endregion

        #region Sub-Menu Creation
        
        #endregion
    }
}
