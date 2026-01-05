/*****************************************************************************
// File Name : IButtonReadable.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Interface that allows a class to be loaded to a combat button/
*****************************************************************************/

using UnityEngine;

namespace COTB.Combat
{
    public interface IButtonReadable
    {
        /// <summary>
        /// Gets the name displayed on the main text of the button.
        /// </summary>
        /// <returns></returns>
        string GetName();
        /// <summary>
        /// Gets the long-form description of the button that appears to the side.
        /// </summary>
        /// <returns></returns>
        string GetDescription();
        /// <summary>
        /// Gets the icon displayed on this button.
        /// </summary>
        /// <returns></returns>
        Sprite GetIcon()
        {
            return null;
        }

        /// <summary>
        /// Called when the button is clicked.
        /// </summary>
        void OnButtonClicked();

        /// <summary>
        /// Checks the current state oof the button and whether it can be clicked or not.
        /// </summary>
        /// <returns>The current buttonState that the button should be in.</returns>
        ButtonState CheckCurrentState();
    }
}
