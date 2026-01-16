/*****************************************************************************
// File Name : CommandButton.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Controls a button used from the character command menu.
*****************************************************************************/
using COTB.Combat.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using COTB.Combat.Actions;

namespace COTB.Combat.UI.CharacterMenu
{
    [RequireComponent(typeof(Button))]
    public class CommandButton : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image icon;
        [Header("Button Settings")]
        [SerializeField] private ActionTags tags;
        [SerializeField] private CharacterCommandMenu commandMenu;
        [SerializeField] private UnityEvent<CombatAction> OnEnabledPress;
        [SerializeField] private UnityEvent OnDisabledPress;

        private ICommandReadable readableData;

        private CommandState buttonState;

        #region Component References
        [Header("Components")]
        [SerializeReference, ReadOnly] private Button unityButton;

        /// <summary>
        /// Get components on reset.
        /// </summary>
        [ContextMenu("Get Component References")]
        private void Reset()
        {
            unityButton = GetComponent<Button>();
        }
        #endregion

        #region Properties
        public Button UnityButton => unityButton;
        public ICommandReadable ReadableData
        {  
            get { return readableData; } 
            set 
            { 
                readableData = value; 
                tags = readableData.GetTags();
            }
        }
        public ActionTags Tags => tags;
        
        #endregion

        private void SetState(CommandState buttonState)
        {
            this.buttonState = buttonState;
        }

        /// <summary>
        /// Sets up this button with information it needs on creation.
        /// </summary>
        /// <remarks>
        /// Only used by spawned buttons.
        /// </remarks>
        /// <param name="buttonData">The button data that this button is based on.</param>
        /// <param name="commandMenu">The action menu that this button belongs to.</param>
        internal void Initialize(ICommandReadable buttonData, CharacterCommandMenu commandMenu)
        {
            ReadableData = buttonData;
            this.commandMenu = commandMenu;
            Debug.Log("Initialized button " + name);
            // If this game object starts enabled, setup event references since the game object is already enabled.
            if (gameObject.activeSelf)
            {
                commandMenu.OnMenuRefreshed += LoadButtonData;
            }
        }

        #region Button Refreshing
        /// <summary>
        /// When this button is enabled, it should listen for action menu refreshes so that
        /// it's information is up to date.
        /// </summary>
        internal void OnButtonEnabled()
        {
            if (commandMenu != null)
            {
                commandMenu.OnMenuRefreshed += LoadButtonData;
            }
        }
        internal void OnButtonDisabled()
        {
            if ( commandMenu != null)
            {
                commandMenu.OnMenuRefreshed -= LoadButtonData;
            }
        }

        /// <summary>
        /// Refreshes and updates this button when the CharacterActionMenu refreshes.
        /// </summary>
        private void LoadButtonData(CharacterCommander targetCharacter)
        {
            if (readableData != null)
            {
                Debug.Log("Loaded button data for button " + name);
                //Debug.Log(readableData.GetName());
                nameText.text = readableData.GetName();
                descriptionText.text = readableData.GetDescription();

                Sprite icn = readableData.GetIcon();
                icon.gameObject.SetActive(icn != null);
                icon.sprite = icn;

                // Set the button's state.
                CommandState state = CommandState.Enabled;   
                if (readableData.GetDisabled())
                {
                    state = CommandState.Disabled;
                }
                if (targetCharacter.CheckLocked(readableData))
                {
                    state = CommandState.Locked;
                }
                SetState(state);
            }
        }
        #endregion


        #region Button Clicking
        /// <summary>
        /// Called from the button UnityEvent and manages behaviour that happens when this button is pressed.
        /// </summary>
        public void OnButtonClicked()
        {
            if (buttonState == CommandState.Enabled)
            {
                OnEnabledPress?.Invoke(AsAction(readableData));
            }
            else
            {
                OnDisabledPress?.Invoke();
            }
        }

        /// <summary>
        /// If this button references an action, extract that action.
        /// </summary>
        /// <param name="readableData">The button's readable data to get the command from.</param>
        /// <returns>The enclosed action, null if not linked to a action.</returns>
        private static CombatAction AsAction(ICommandReadable readableData)
        {
            if (readableData is CombatAction cmd)
            {
                return cmd;
            }
            else if (readableData is ActionCommand action)
            {
                return action.Action;
            }
            return null;
        }
        
        /// <summary>
        /// Adds a UnityAction for the OnEnabledPress event of this button to call.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void AddEnabledListener(UnityAction<CombatAction> action)
        {
            OnEnabledPress.AddListener(action);
        }

        /// <summary>
        /// Don't know if this is needed, but remove all added listeners from the enabled
        /// press event when the button is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            OnEnabledPress.RemoveAllListeners();
            if (commandMenu != null)
            {
                commandMenu.OnMenuRefreshed -= LoadButtonData;
            }
        }
        #endregion
    }
}
