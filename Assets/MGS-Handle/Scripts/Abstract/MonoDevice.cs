/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoDevice.cs
 *  Description  :  Define MonoDevice.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Device
{
    /// <summary>
    /// Device component.
    /// </summary>
    public abstract class MonoDevice : MonoBehaviour, IDevice
    {
        #region Property
        /// <summary>
        /// Enable to control.
        /// </summary>
        public abstract bool IsEnable { set; get; }
        #endregion
    }
}