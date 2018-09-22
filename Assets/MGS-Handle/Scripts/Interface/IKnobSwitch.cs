/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IKnobSwitch.cs
 *  Description  :  Interface for knob switch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Mathematics;
using System;
using System.Collections.Generic;

namespace Mogoson.Device
{
    /// <summary>
    /// Mouse Axis.
    /// </summary>
    public enum MouseAxis
    {
        MouseX = 0,
        MouseY = 1
    }

    /// <summary>
    /// Interface for knob switch.
    /// </summary>
    public interface IKnobSwitch : IDevice
    {
        #region Property
        /// <summary>
        /// Mouse input axis.
        /// </summary>
        MouseAxis MouseInput { set; get; }

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        float RotateSpeed { set; get; }

        /// <summary>
        /// Limit rotate angle.
        /// </summary>
        bool RotateLimit { set; get; }

        /// <summary>
        /// Interval of rotate angle.
        /// </summary>
        Interval AngleInterval { set; get; }

        /// <summary>
        /// Adsorbent to target angle on mouse up.
        /// </summary>
        bool Adsorbent { set; get; }

        /// <summary>
        /// Target adsorbent angles.
        /// </summary>
        float[] AdsorbentAngles { set; get; }

        /// <summary>
        /// Switch current angle.
        /// </summary>
        float Angle { get; }

        /// <summary>
        /// Switch current rotate percent base range.
        /// </summary>
        float Percent { get; }
        #endregion

        #region Event
        /// <summary>
        /// Knob switch drag event.
        /// </summary>
        event Action OnSwitchDrag;

        /// <summary>
        /// Knob switch release event.
        /// </summary>
        event Action OnSwitchRelease;

        /// <summary>
        /// Knob switch adsorbent event.
        /// </summary>
        event Action OnSwitchAdsorbent;
        #endregion
    }
}