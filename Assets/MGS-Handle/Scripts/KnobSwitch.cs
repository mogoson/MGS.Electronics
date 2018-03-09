/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  KnobSwitch.cs
 *  Description  :  Define knob switch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace Developer.Handle
{
    /// <summary>
    /// Mouse Axis.
    /// </summary>
    public enum MouseAxis
    {
        MouseX = 0,
        MouseY = 1
    }

    [AddComponentMenu("Developer/Handle/KnobSwitch")]
    [RequireComponent(typeof(Collider))]
    public class KnobSwitch : MonoBehaviour
    {
        #region Field and Property 
        /// <summary>
        /// Enable control.
        /// </summary>
        public bool isEnable = true;

        /// <summary>
        /// Mouse input axis.
        /// </summary>
        public MouseAxis mouseInput;

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        public float rotateSpeed = 250;

        /// <summary>
        /// Limit rotate angle range.
        /// </summary>
        public bool rangeLimit = false;

        /// <summary>
        /// Min rotate angle.
        /// </summary>
        public float minAngle = -60;

        /// <summary>
        /// Max rotate angle.
        /// </summary>
        public float maxAngle = 60;

        /// <summary>
        /// Adsorbent to target angle on mouse up.
        /// </summary>
        public bool adsorbent = false;

        /// <summary>
        /// Target adsorbent angles.
        /// </summary>
        public float[] adsorbentAngles;

        /// <summary>
        /// Switch current angle.
        /// </summary>
        public float Angle { protected set; get; }

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 StartAngles { protected set; get; }

        /// <summary>
        /// Switch current rotate percent base range.
        /// </summary>
        public float Percent
        {
            get
            {
                if (rangeLimit)
                {
                    var range = maxAngle - minAngle;
                    return (Angle - minAngle) / (range == 0 ? 1 : range);
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// Knob switch drag event.
        /// </summary>
        public event Action OnSwitchDrag;

        /// <summary>
        /// Knob switch release event.
        /// </summary>
        public event Action OnSwitchRelease;

        /// <summary>
        /// Knob switch adsorbent event.
        /// </summary>
        public event Action OnSwitchAdsorbent;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            StartAngles = transform.localEulerAngles;
        }

        /// <summary>
        /// Response mouse left button drag.
        /// </summary>
        protected virtual void OnMouseDrag()
        {
            if (!isEnable)
                return;

            Angle += GetMouseInput() * rotateSpeed * Time.deltaTime;
            if (rangeLimit)
                Angle = Mathf.Clamp(Angle, minAngle, maxAngle);
            RotateKnob(Angle);

            if (OnSwitchDrag != null)
                OnSwitchDrag.Invoke();
        }

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
                return;

            if (OnSwitchRelease != null)
                OnSwitchRelease.Invoke();

            if (!adsorbent || adsorbentAngles.Length == 0)
                return;

            var nearAngle = 0f;
            var tempNear = float.PositiveInfinity;
            foreach (var adsorbentAngle in adsorbentAngles)
            {
                var deltaAngle = Mathf.Abs(Angle - adsorbentAngle);
                if (deltaAngle < tempNear)
                {
                    tempNear = deltaAngle;
                    nearAngle = adsorbentAngle;
                }
            }
            Angle = nearAngle;
            RotateKnob(Angle);

            if (OnSwitchAdsorbent != null)
                OnSwitchAdsorbent.Invoke();
        }

        /// <summary>
        /// Rotate knob switch to target angle.
        /// </summary>
        /// <param name="rotateAngle">Rotate angle.</param>
        protected virtual void RotateKnob(float rotateAngle)
        {
            transform.localRotation = Quaternion.Euler(StartAngles + Vector3.back * rotateAngle);
        }

        /// <summary>
        /// Get mouse input.
        /// </summary>
        /// <returns>Mouse input.</returns>
        protected float GetMouseInput()
        {
            if (mouseInput == MouseAxis.MouseX)
                return Input.GetAxis("Mouse X");
            else
                return Input.GetAxis("Mouse Y");
        }
        #endregion
    }
}