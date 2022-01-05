/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ILED.cs
 *  Description  :  Interface for LED.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Electronics
{
    /// <summary>
    /// Interface for LED.
    /// </summary>
    public interface ILED : IELEElement
    {
        #region Method
        /// <summary>
        /// Turn on LED.
        /// </summary>
        void TurnOn();

        /// <summary>
        /// Turn off LED.
        /// </summary>
        void TurnOff();
        #endregion
    }
}