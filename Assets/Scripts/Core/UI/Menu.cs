/*****************************************************************************
// File Name : Menu.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Abstract base class for scripts that control menus.
*****************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace COTB.UI
{
    public abstract class Menu : MonoBehaviour
    {
        #region Vars
        [SerializeField, Tooltip("The button to be selected when this menu is loaded.")]
        protected Button initialButton;

        //protected bool isEnabled;
        #endregion

        #region Properties
        //public bool IsEnabled
        //{
        //    get
        //    {
        //        return isEnabled;
        //    }
        //}
        #endregion

        /// <summary>
        /// Sets up this object with information. 
        /// </summary>
        /// <param name="initButton"> The button that should be initially selected when the menu is opened. </param>
        /// <param name="numberOfButtons"> The number of buttons in this menu. </param>
        /// <param name="menuName"> The name of this menu.  For hierarchy organizational purposes only. </param>
        //public virtual void Initialize(Button initButton, int numberOfButtons, string menuName)
        //{
        //    m_initialButton = initButton;
        //    //ScaleContent(numberOfButtons);
        //    gameObject.name = menuName;
        //}

        /// <summary>
        /// Toggles this menu's visibility on and off.
        /// </summary>
        /// <remarks>
        /// Use this to toggle it on/off temporarily.  Use Load or Unload when it is beginning to be used/ends its use.
        /// </remarks>
        /// <param name="enabled"> Whether to enable or disable this menu.</param>
        public virtual void ToggleMenu(bool enabled)
        {
            //isEnabled = enabled;
            gameObject.SetActive(enabled);
        }

        /// <summary>
        /// Completely enables the menu for use.
        /// </summary>
        public virtual void Load()
        {
            ToggleMenu(true);
            /// Need to select the new button after the menu has been loaded because disabled game objects dont recieve selection messages.
            initialButton.Select();
        }

        /// <summary>
        /// Ends the this menu's use.
        /// </summary>
        public virtual void Unload()
        {
            // Had this in because I thought .Select doesnt call IDeselectHandley automatically, but it does.
            // The problem was that a message cant be sent to a disabled game object
            //ExecuteEvents.Execute<IDeselectHandler>(EventSystem.current.currentSelectedGameObject, null, (x, y) => x.OnDeselect(new BaseEventData(EventSystem.current)));
            EventSystem.current.SetSelectedGameObject(null);
            ToggleMenu(false);
        }

        ///// <summary>
        ///// Scales the content object for this menu to account for the number of buttons it has.
        ///// </summary>
        ///// <remarks>
        ///// Ensures that the Content always is just the right height to contain all the buttons and no more. 
        ///// </remarks>
        ///// <param name="buttonNum"></param>
        //private void ScaleContent(int buttonNum)
        //{
        //    GridLayoutGroup gridLayout = ContentObject.GetComponent<GridLayoutGroup>();
        //    if (gridLayout == null)
        //    {
        //        return;
        //    }

        //    /// Calculate the height that the content object needs to be based on the number of buttons and how much space each one
        //    /// takes up based on the settings of the Grid Layout Group.
        //    RectTransform rectTrans = ContentObject.transform as RectTransform;
        //    Vector2 newSize = rectTrans.sizeDelta;
        //    float size = (gridLayout.cellSize.y + gridLayout.spacing.y) * buttonNum + gridLayout.padding.top + gridLayout.padding.bottom;
        //    newSize.y += size;
        //    rectTrans.sizeDelta = newSize;
        //}
    }
}