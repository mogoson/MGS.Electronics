/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HandleLED.cs
 *  Description  :  Define LED of handle.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Handle
{
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