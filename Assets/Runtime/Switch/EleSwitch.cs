/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleSwitch.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/20/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Electronics
{
    public class EleSwitch<T> : Electronic, IEleSwitch<T>
    {
        [SerializeField] protected bool isInteractable = true;

        public event Action<T> OnSwitch;

        public virtual bool IsInteractable
        {
            set { isInteractable = value; }
            get { return isInteractable; }
        }

        protected void InvokeOnSwitch(T state)
        {
            OnSwitch?.Invoke(state);
        }
    }
}