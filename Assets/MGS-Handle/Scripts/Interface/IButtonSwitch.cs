/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IButtonSwitch.cs
 *  Description  :  Interface for button switch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace Mogoson.Device
{
    /// <summary>
    /// Interface for button switch.
    /// </summary>
    public interface IButtonSwitch : IDevice
    {
        #region Property
        /// <summary>
        /// Switch down offset.
        /// </summary>
        float DownOffset { set; get; }

        /// <summary>
        /// Self lock on switch down.
        /// </summary>
        bool SelfLock { set; get; }

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        float LockPercent { set; get; }

        /// <summary>
        /// Toggle LED on toggle button.
        /// </summary>
        bool UseLED { set; get; }

        /// <summary>
        /// LED of button switch.
        /// </summary>
        ILED LED { set; get; }

        /// <summary>
        /// Button switch is down state.
        /// </summary>
        bool IsDown { get; }
        #endregion

        #region Event
        /// <summary>
        /// Button switch up event.
        /// </summary>
        event Action OnSwitchUp;

        /// <summary>
        /// Button switch down event.
        /// </summary>
        event Action OnSwitchDown;

        /// <summary>
        /// Button switch lock event.
        /// </summary>
        event Action OnSwitchLock;
        #endregion
    }
}