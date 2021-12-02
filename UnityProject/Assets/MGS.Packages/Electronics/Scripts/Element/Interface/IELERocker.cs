/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IELERocker.cs
 *  Description  :  Interface for electronic rocker.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Electronics
{
    /// <summary>
    /// Interface for electronic rocker.
    /// </summary>
    public interface IELERocker : IELEElement
    {
        #region Property
        /// <summary>
        /// Radius angle.
        /// </summary>
        float RadiusAngle { set; get; }

        /// <summary>
        /// Rocker rotate speed.
        /// </summary>
        float RotateSpeed { set; get; }

        /// <summary>
        /// Revert speed.
        /// </summary>
        float RevertSpeed { set; get; }

        /// <summary>
        /// Rocker current angles normalized vector.
        /// </summary>
        Vector2 Vector { get; }
        #endregion

        #region Event
        /// <summary>
        /// Rocker drag event.
        /// </summary>
        event Action OnDragEvent;

        /// <summary>
        /// Rocker Release event.
        /// </summary>
        event Action OnReleaseEvent;

        /// <summary>
        /// Rocker revert event.
        /// </summary>
        event Action OnRevertEvent;
        #endregion
    }
}