/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IEleSwitch.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/20/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Electronics
{
    public interface IEleSwitch<T> : IElectronic
    {
        event Action<T> OnSwitch;

        bool IsInteractable { set; get; }
    }
}