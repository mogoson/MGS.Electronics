/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IELEElement.cs
 *  Description  :  Interface for electronic element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Electronics
{
    /// <summary>
    /// Interface for electronic element.
    /// </summary>
    public interface IELEElement
    {
        #region Property
        /// <summary>
        /// Electronic element is active?
        /// </summary>
        bool IsActive { set; get; }
        #endregion
    }
}