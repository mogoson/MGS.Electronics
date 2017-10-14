/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Handle.cs
 *  Description  :  Define MouseAxis and HandleEvent.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *************************************************************************/

namespace Developer.Handle
{
    /// <summary>
    /// Mouse Axis.
    /// </summary>
    public enum MouseAxis
    {
        MouseX, MouseY
    }

    /// <summary>
    /// Handle Event.
    /// </summary>
    public delegate void HandleEvent();
}