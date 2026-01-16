/*****************************************************************************
// File Name : CombatButton.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Controls a button used from the character action menu.
*****************************************************************************/
using COTB.Combat.Characters;
using COTB.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace COTB.Combat.UI.CharacterMenu
{
    [RequireComponent(typeof(Button))]
    public class CharacterButton : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image icon;
        [Header("Button Settings")]
        [SerializeField] private ActionTags tags;
        [SerializeField] private CharacterActionMenu actionMenu;
        [SerializeField] private UnityEvent<Command> OnEnabledPress;
        [SerializeField] private UnityEvent OnDisabledPress;

        private ICommanderReadable readableData;

        private ActionState buttonState;

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
        public ICommanderReadable ReadableData
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

        private void SetState(ActionState buttonState)
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
        /// <param name="actionMenu">The action menu that this button belongs to.</param>
        public void Initialize(ICommanderReadable buttonData, CharacterActionMenu actionMenu)
        {
            ReadableData = buttonData;
            this.actionMenu = actionMenu;
        }

        #region Button Refreshing
        /// <summary>
        /// When this button is enabled, it should listen for action menu refreshes so that
        /// it's information is up to date.
        /// </summary>
        private void OnEnable()
        {
            actionMenu.OnMenuRefreshed += LoadButtonData;
        }
        private void OnDisable()
        {
            actionMenu.OnMenuRefreshed -= LoadButtonData;
        }

        /// <summary>
        /// Refreshes and updates this button when the CharacterActionMenu refreshes.
        /// </summary>
        private void LoadButtonData(CharacterCommander targetCharacter)
        {
            if (readableData != null)
            {
                //Debug.Log(readableData.GetName());
                nameText.text = readableData.GetName();
                descriptionText.text = readableData.GetDescription();

                Sprite icn = readableData.GetIcon();
                icon.gameObject.SetActive(icn != null);
                icon.sprite = icn;

                // Set the button's state.
                ActionState state = ActionState.Enabled;   
                if (readableData.GetDisabled())
                {
                    state = ActionState.Disabled;
                }
                if (targetCharacter.CheckLocked(readableData))
                {
                    state = ActionState.Locked;
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
            if (buttonState == ActionState.Enabled)
            {
                OnEnabledPress?.Invoke(AsCommand(readableData));
            }
            else
            {
                OnDisabledPress?.Invoke();
            }
        }

        /// <summary>
        /// If this button references a command, extract that command
        /// </summary>
        /// <param name="readableData">The button's readable data to get the command from.</param>
        /// <returns>The enclosed command, null if not linked to a command.</returns>
        private static Command AsCommand(ICommanderReadable readableData)
        {
            if (readableData is Command cmd)
            {
                return cmd;
            }
            else if (readableData is Characters.CommandAction action)
            {
                return action.Command;
            }
            return null;
        }
        
        /// <summary>
        /// Adds a UnityAction for the OnEnabledPress event of this button to call.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void AddEnabledListener(UnityAction<Command> action)
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
        }
        #endregion
    }
}
