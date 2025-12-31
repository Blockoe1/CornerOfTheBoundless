/*****************************************************************************
// File Name : ButtonOrganizer.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Organizes the button's hierarchy information in one place.
*****************************************************************************/
using TMPro;
using UnityEngine;

namespace COTB
{
    public class ButtonOrganizer : MonoBehaviour
    {
        [SerializeField] private string buttonName;
        [SerializeField] private TMP_Text buttonText;

        /// <summary>
        /// Update the button's text and name to reflect the name.
        /// </summary>
        private void OnValidate()
        {
            gameObject.name = buttonName + "Button";
            if (buttonText != null )
            {
                buttonText.text = buttonName;
                buttonText.gameObject.name = buttonName + "ButtonText";
            }
        }
    }
}
