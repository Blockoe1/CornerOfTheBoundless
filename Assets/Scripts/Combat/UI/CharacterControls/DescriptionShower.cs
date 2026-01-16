/*****************************************************************************
// File Name : DescriptionShower.cs
// Author : Eli Koederitz
// Creation Date : 1/16/2026
// Last Modified : 1/16/2026
//
// Brief Description : Shows a button's description when it is selected.
*****************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace COTB.Combat.UI
{
    public class DescriptionShower : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private GameObject descriptionGo;

        /// <summary>
        /// Show the description when the button is selected, hide it when deselected.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnSelect(BaseEventData eventData)
        {
            ToggleDescription(true);
        }
        public void OnDeselect(BaseEventData eventData)
        {
            ToggleDescription(false);
        }

        /// <summary>
        /// Toggles the description.
        /// </summary>
        /// <param name="isEnabled"></param>
        private void ToggleDescription(bool isEnabled)
        {
            if (descriptionGo != null)
            {
                descriptionGo.SetActive(isEnabled);
            }
        }
    }
}
