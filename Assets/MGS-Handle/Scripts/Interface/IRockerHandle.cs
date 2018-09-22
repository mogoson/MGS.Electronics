/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IRockerHandle.cs
 *  Description  :  Interface for rocker handle.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Device
{
    /// <summary>
    /// Interface for rocker handle.
    /// </summary>
    public interface IRockerHandle : IDevice
    {
        #region Property
        /// <summary>
        /// Radius angle.
        /// </summary>
        float RadiusAngle { set; get; }

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        float RotateSpeed { set; get; }

        /// <summary>
        /// Revert speed.
        /// </summary>
        float RevertSpeed { set; get; }

        /// <summary>
        /// Handle out put normalized vector.
        /// </summary>
        Vector2 Vector { get; }
        #endregion

        #region Event
        /// <summary>
        /// Handle drag event.
        /// </summary>
        event Action OnHandleDrag;

        /// <summary>
        /// Handle Release event.
        /// </summary>
        event Action OnHandleRelease;

        /// <summary>
        /// Handle revert event.
        /// </summary>
        event Action OnHandleRevert;
        #endregion
    }
}