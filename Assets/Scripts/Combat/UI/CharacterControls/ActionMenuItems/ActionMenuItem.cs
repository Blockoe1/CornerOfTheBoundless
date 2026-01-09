/*****************************************************************************
// File Name : ActionMenuItem.cs
// Author : Eli Koederitz
// Creation Date : 1/4/2026
// Last Modified : 1/4/2026
//
// Brief Description : Abstract base class that defines a specific button on the action menu.
*****************************************************************************/
using UnityEngine;

namespace COTB.Combat.UI.CharacterControls
{
    public abstract class ActionMenuItem : MonoBehaviour, IButtonReadable
    {
        [Header("Button Settings")]
        [SerializeField] private CombatButton buttonPrefab;
        [SerializeField] protected string buttonName;
        [SerializeField, TextArea] private string buttonDescription;
        [SerializeField] private Sprite buttonIcon;
        [SerializeField, Tooltip("Controls the order that this button is placed compared to other custom " +
            "ActionMenuItems")] 
        private int buttonIndex;

        private ButtonState currentItemState = ButtonState.Enabled;
        protected CombatButton baseButton;
        protected CharacterCommander commander;

        #region Properties
        internal int ButtonIndex => buttonIndex;
        public ButtonState currentState { get { return currentItemState; } set { currentItemState = value; } }
        #endregion

        #region Button Readable Functions
        /// <summary>
        /// Buttons linked to menuItems are enabled by default.
        /// </summary>
        /// <returns>The default button state.</returns>
        public virtual ButtonState CheckCurrentState()
        {
            return currentItemState;
        }

        /// <summary>
        /// Gets the name to display on the buttons main text.
        /// </summary>
        public string GetName()
        {
            return buttonName;
        }

        /// <summary>
        /// Gets the popout description to display next to the button.
        /// </summary>
        public string GetDescription()
        {
            return buttonDescription;
        }

        /// <summary>
        /// Gets the icon sprite to display on this button.
        /// </summary>
        public Sprite GetIcon()
        {
            return buttonIcon;
        }

        #endregion

        /// <summary>
        /// Sets up the UI for this custom action item.
        /// </summary>
        /// <param name="actionMenu"></param>
        public virtual void Initialize(CharacterActionMenu actionMenu, CharacterCommander commander)
        {
            this.commander = commander;
            // Creates the button on the action menu content.
            baseButton = Instantiate(buttonPrefab, actionMenu.Content);
            // Temp Code.  Sets the button to be the second child of the content.  This way, it's after attack, and
            // ActionMenuItem Initialize functions will be run in reverse buttonIndex order so the highest index item
            // gets made last and ends up at the highest index and doesnt push anything down.
            baseButton.transform.SetSiblingIndex(1);
            baseButton.gameObject.SetActive(false);
            baseButton.Initialize(this, actionMenu.ScrollController, actionMenu.RootMenu);
        }

        /// <summary>
        /// Cleans up any spawned GameObjects when this object goes out of scope.
        /// </summary>
        public virtual void CleanUp()
        {
            // Destroys the button created for this item.
            Destroy(baseButton);
        }

        /// <summary>
        /// Controls showing the corresponding buttons when this character is selected.
        /// </summary>
        public virtual void OnSelected()
        {
            baseButton.gameObject.SetActive(true);
        }
        /// <summary>
        /// Controls hiding this character's buttons from the menu when they are no longer selected.
        /// </summary>
        public virtual void OnDeselected()
        {
            baseButton.gameObject.SetActive(false);
        }

        #region Abstract Blueprint
        /// <summary>
        /// Overrideable function that controls what happens when base button for this menu item is clicked.
        /// </summary>
        public abstract void OnButtonClicked();
        #endregion
    }
}
