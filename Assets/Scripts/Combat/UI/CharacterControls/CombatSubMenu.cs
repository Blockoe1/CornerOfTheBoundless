/*****************************************************************************
// File Name : CombatSubMenu.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Specialized sub-menu for the character action menu.
*****************************************************************************/
using COTB.UI;
using UnityEngine;
using UnityEngine.UI;

namespace COTB.Combat.UI.CharacterControls
{
    public class CombatSubMenu : SubMenu
    {
        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private ScrollWithSelected scrollController;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            scrollController = GetComponent<ScrollWithSelected>();
        }
        #endregion

        #region Properties
        public ScrollWithSelected ScrollController => scrollController;
        #endregion

        /// <summary>
        /// Sets up this menu after buttons are created.
        /// </summary>
        /// <param name="initialButton">The initial button that starts out selected when opened.</param>
        /// <param name="parentButton">The parent button that opens this menu.</param>
        /// <param name="numButtons">The number of individual buttons in the menu.</param>
        /// <param name="menuName">The name of the menu (for hierarchy organization).</param>
        public void Initialize(Button initialButton, Button parentButton, int numButtons, string menuName)
        {
            ScaleContent(numButtons);
            this.initialButton = initialButton;
            this.parentButton = parentButton;
            gameObject.name = menuName;
        }

        /// <summary>
        /// Scales the content object for this menu to account for the number of buttons it has.
        /// </summary>
        /// <remarks>
        /// Ensures that the Content always is just the right height to contain all the buttons and no more. 
        /// </remarks>
        /// <param name="buttonNum"></param>
        private void ScaleContent(int buttonNum)
        {
            if (Content.TryGetComponent(out GridLayoutGroup grid))
            {
                /// Calculate the height that the content object needs to be based on the number of buttons and how much space each one
                /// takes up based on the settings of the Grid Layout Group.
                RectTransform rectTrans = Content as RectTransform;
                Vector2 newSize = rectTrans.sizeDelta;
                float size = (grid.cellSize.y + grid.spacing.y) * buttonNum + grid.padding.top + grid.padding.bottom;
                newSize.y += size;
                rectTrans.sizeDelta = newSize;
            }
        }
    }
}
