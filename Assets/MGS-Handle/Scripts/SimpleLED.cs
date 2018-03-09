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

namespace Developer.Handle
{
    [AddComponentMenu("Developer/Handle/SimpleLED")]
    [RequireComponent(typeof(Renderer))]
    public class SimpleLED : HandleLED
    {
        #region Field and Property 
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
            LEDRenderer.material = highlightMat;
        }

        /// <summary>
        /// Close LED.
        /// </summary>
        public override void Close()
        {
            LEDRenderer.material = defaultMat;
        }
        #endregion
    }
}