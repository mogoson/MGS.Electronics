/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleLight.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/20/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Electronics
{
    [AddComponentMenu("MGS/Electronic/Ele Light")]
    [RequireComponent(typeof(Renderer))]
    public class EleLight : Electronic, IEleLight
    {
        [HideInInspector]
        [SerializeField] protected Renderer render;
        [SerializeField] protected Material material;
        protected Material originMat;

        public bool IsOn
        {
            set
            {
                if (isOn != value)
                {
                    isOn = value;
                    SetLight(isOn);
                }
            }
            get { return isOn; }
        }
        protected bool isOn;

        protected virtual void Reset()
        {
            render = GetComponent<Renderer>();
        }

        protected virtual void Awake()
        {
            originMat = render.material;
        }

        protected virtual void SetLight(bool isOn)
        {
            var mat = isOn ? material : originMat;
            render.material = mat;
        }
    }
}