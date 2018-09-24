/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoLED.cs
 *  Description  :  Define LED of device.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace Mogoson.Device
{
    /// <summary>
    /// LED of device.
    /// </summary>
    public abstract class MonoLED : MonoDevice, ILED
    {
        #region Public Method
        /// <summary>
        /// Open LED.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// Close LED.
        /// </summary>
        public abstract void Close();
        #endregion
    }
}