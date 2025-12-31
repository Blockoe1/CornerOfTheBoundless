/*****************************************************************************
// File Name : IButtonReadable.cs
// Author : Eli Koederitz
// Creation Date : 12/31/2025
// Last Modified : 12/31/2025
//
// Brief Description : Interface that allows a class to be loaded to a combat button/
*****************************************************************************/

namespace COTB.Combat
{
    public interface IButtonReadable
    {
        string GetName();
        string GetDescription();
        string GetIcon();
        string GetCost();

        void OnButtonClicked();
    }
}
