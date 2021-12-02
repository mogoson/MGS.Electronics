/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoELEElement.cs
 *  Description  :  Define MonoELEElement.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Electronics
{
    /// <summary>
    /// Electronic element.
    /// </summary>
    public abstract class MonoELEElement : MonoBehaviour, IELEElement
    {
        #region Field and Property
        /// <summary>
        /// Electronic element is active?
        /// </summary>
        [Tooltip("Electronic element is active?")]
        [SerializeField]
        protected bool isActive = true;

        /// <summary>
        /// Electronic element is active?
        /// </summary>
        public bool IsActive
        {
            set { isActive = value; }
            get { return isActive; }
        }
        #endregion
    }
}