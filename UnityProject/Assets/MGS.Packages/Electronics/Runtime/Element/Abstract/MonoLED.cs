/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoLED.cs
 *  Description  :  Define LED element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Electronics
{
    /// <summary>
    /// LED element.
    /// </summary>
    public abstract class MonoLED : MonoELEElement, ILED
    {
        #region Public Method
        /// <summary>
        /// Open LED.
        /// </summary>
        public abstract void TurnOn();

        /// <summary>
        /// Close LED.
        /// </summary>
        public abstract void TurnOff();
        #endregion
    }
}