/*****************************************************************************
// File Name : CombatButton.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Controls a button used from the character action menu.
*****************************************************************************/
using COTB.UI;
using CustomAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace COTB.Combat.UI
{
    public class CombatButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Image icon;

        private IButtonReadable readableData;
        private Menu parentMenu;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private ScrollSelectDetector detector;
        [SerializeReference, ReadOnly] private Button linkedButton;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            detector = GetComponent<ScrollSelectDetector>();
            linkedButton = GetComponent<Button>();
        }
        #endregion

        #region Properties
        public Button LinkedButton => linkedButton;
        #endregion

        /// <summary>
        /// Sets up this button with information it needs on creation.
        /// </summary>
        /// <param name="buttonData">The button data that this button is based on.</param>
        /// <param name="parentScrollController">The scroll controller that this button belongs to.</param>
        /// <param name="parentMenu">menu this button belongs to.</param>
        public void Initialize(IButtonReadable buttonData, ScrollWithSelected parentScrollController, Menu parentMenu)
        {
            detector.ScrollController = parentScrollController;
            readableData = buttonData;
            this.parentMenu = parentMenu;
            if (parentMenu != null)
            {
                parentMenu.OnMenuLoaded += LoadButtonData;
            }
            LoadButtonData();
        }

        /// <summary>
        /// Refreshes and updates this button when the menu is loaded.
        /// </summary>
        private void LoadButtonData()
        {
            if (readableData != null)
            {
                nameText.text = readableData.GetName();
                descriptionText.text = readableData.GetDescription();

                int cost = readableData.GetCost();
                costText.gameObject.SetActive(cost > 0);
                costText.text = cost.ToString();

                Sprite icn = readableData.GetIcon();
                icon.gameObject.SetActive(icn != null);
                icon.sprite = icn;
            }
        }

        /// <summary>
        /// Unsubscribe events.
        /// </summary>
        private void OnDestroy()
        {
            if (parentMenu != null)
            {
                parentMenu.OnMenuLoaded -= LoadButtonData;
            }
        }
    }
}
