/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IELEEquipment.cs
 *  Description  :  Interface for electronic equipment.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/26/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Electronics
{
    /// <summary>
    /// Interface for electronic equipment.
    /// </summary>
    public interface IELEEquipment
    {
        #region Method
        /// <summary>
        /// Turn on equipment.
        /// </summary>
        void TurnOn();

        /// <summary>
        /// Turn off equipment.
        /// </summary>
        void TurnOff();
        #endregion
    }
}