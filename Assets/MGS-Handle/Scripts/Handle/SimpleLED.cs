/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SimpleLED.cs
 *  Description  :  Define simple LED.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Device
{
    /// <summary>
    /// Simple LED for handle.
    /// </summary>
    [AddComponentMenu("Mogoson/Device/SimpleLED")]
    [RequireComponent(typeof(Renderer))]
    public class SimpleLED : MonoLED
    {
        #region Field and Property
        /// <summary>
        /// Is enable to control.
        /// </summary>
        [SerializeField]
        protected bool isEnable = true;

        /// <summary>
        /// Highlight material of LED.
        /// </summary>
        public Material highlightMat;

        /// <summary>
        /// Default material of LED.
        /// </summary>
        protected Material defaultMat;

        /// <summary>
        /// Renderer of LED.
        /// </summary>
        protected Renderer LEDRenderer;

        /// <summary>
        /// Enable to control LED.
        /// </summary>
        public override bool IsEnable
        {
            set { isEnable = value; }
            get { return isEnable; }
        }
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            LEDRenderer = GetComponent<Renderer>();
            defaultMat = LEDRenderer.material;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Open LED.
        /// </summary>
        public override void Open()
        {
            if (isEnable)
            {
                LEDRenderer.material = highlightMat;
            }
        }

        /// <summary>
        /// Close LED.
        /// </summary>
        public override void Close()
        {
            if (isEnable)
            {
                LEDRenderer.material = defaultMat;
            }
        }
        #endregion
    }
}