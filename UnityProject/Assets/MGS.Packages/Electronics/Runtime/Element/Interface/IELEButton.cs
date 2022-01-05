/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IELEButton.cs
 *  Description  :  Interface for electronic button.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Electronics
{
    /// <summary>
    /// Interface for electronic button.
    /// </summary>
    public interface IELEButton : IELEElement
    {
        #region Property
        /// <summary>
        /// Switch down offset.
        /// </summary>
        float DownOffset { set; get; }

        /// <summary>
        /// Self lock on button down?
        /// </summary>
        bool SelfLock { set; get; }

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        float LockPercent { set; get; }

        /// <summary>
        /// Toggle LED on toggle button?
        /// </summary>
        bool UseLED { set; get; }

        /// <summary>
        /// LED of button.
        /// </summary>
        ILED LED { set; get; }

        /// <summary>
        /// Button is down?
        /// </summary>
        bool IsDown { get; }
        #endregion

        #region Event
        /// <summary>
        /// Button up event.
        /// </summary>
        event Action OnUpEvent;

        /// <summary>
        /// Button down event.
        /// </summary>
        event Action OnDownEvent;

        /// <summary>
        /// Button lock event.
        /// </summary>
        event Action OnLockEvent;
        #endregion
    }
}