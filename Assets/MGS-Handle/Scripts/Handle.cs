/*************************************************************************
 *  Copyright (C), 2016-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Handle.cs
 *  Description  :  Define MouseAxis, HandleEvent and HandleLight.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  1/16/2018
 *  Description  :  Add Class HandleLED to define LED of handle.
 *************************************************************************/

using UnityEngine;

namespace Developer.Handle
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
    /// Handle Event.
    /// </summary>
    public delegate void HandleEvent();

    /// <summary>
    /// LED of handle.
    /// </summary>
    public abstract class HandleLED : MonoBehaviour
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