/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IEleLight.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/19/2025
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Electronics
{
    public interface IEleLight : IElectronic
    {
        bool IsOn { set; get; }
    }
}