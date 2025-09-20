/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Electronic.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/19/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Electronics
{
    [AddComponentMenu("MGS/Electronic/Electronic")]
    public class Electronic : MonoBehaviour, IElectronic
    {
        public bool IsEnable
        {
            set { enabled = value; }
            get { return enabled; }
        }

        public bool IsActive
        {
            set { gameObject.SetActive(value); }
            get { return gameObject.activeSelf; }
        }
    }
}