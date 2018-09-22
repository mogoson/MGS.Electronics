/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IDevice.cs
 *  Description  :  Interface for device.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace Mogoson.Device
{
    /// <summary>
    /// Interface for device.
    /// </summary>
    public interface IDevice
    {
        #region Property
        /// <summary>
        /// Enable to control.
        /// </summary>
        bool IsEnable { set; get; }
        #endregion
    }
}