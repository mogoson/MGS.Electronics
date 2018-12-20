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

namespace Mogoson.Device
{
    /// <summary>
    /// Switch with knob.
    /// </summary>
    [AddComponentMenu("Mogoson/Device/KnobSwitch")]
    [RequireComponent(typeof(Collider))]
    public class KnobSwitch : MonoDevice, IKnobSwitch
    {
        #region Field and Property 
        /// <summary>
        /// Enable to control.
        /// </summary>
        [SerializeField]
        protected bool isEnable = true;

        /// <summary>
        /// Mouse input axis.
        /// </summary>
        [SerializeField]
        protected MouseAxis mouseInput;

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        [SerializeField]
        protected float rotateSpeed = 250;

        /// <summary>
        /// Limit rotate angle.
        /// </summary>
        [SerializeField]
        protected bool rotateLimit = false;

        /// <summary>
        /// Range of rotate angle.
        /// </summary>
        [SerializeField]
        protected Range angleRange = new Range(-60, 60);

        /// <summary>
        /// Adsorbent to target angle on mouse up.
        /// </summary>
        [SerializeField]
        protected bool adsorbent = false;

        /// <summary>
        /// Adsorbable angles.
        /// </summary>
        [SerializeField]
        protected float[] adsorbableAngles;

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 StartAngles { protected set; get; }

        /// <summary>
        /// Enable to control.
        /// </summary>
        public override bool IsEnable
        {
            set { isEnable = value; }
            get { return isEnable; }
        }

        /// <summary>
        /// Mouse input axis.
        /// </summary>
        public MouseAxis MouseInput
        {
            set { mouseInput = value; }
            get { return mouseInput; }
        }

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        public float RotateSpeed
        {
            set { rotateSpeed = value; }
            get { return rotateSpeed; }
        }

        /// <summary>
        /// Limit rotate angle.
        /// </summary>
        public bool RotateLimit
        {
            set { rotateLimit = value; }
            get { return rotateLimit; }
        }

        /// <summary>
        /// Range of rotate angle.
        /// </summary>
        public Range AngleRange
        {
            set { angleRange = value; }
            get { return angleRange; }
        }

        /// <summary>
        /// Adsorbent to target angle on mouse up.
        /// </summary>
        public bool Adsorbent
        {
            set { adsorbent = value; }
            get { return adsorbent; }
        }

        /// <summary>
        /// Adsorbable angles.
        /// </summary>
        public float[] AdsorbableAngles
        {
            set { adsorbableAngles = value; }
            get { return adsorbableAngles; }
        }

        /// <summary>
        /// Switch current angle.
        /// </summary>
        public float Angle { protected set; get; }

        /// <summary>
        /// Switch current rotate percent base range.
        /// </summary>
        public float Percent
        {
            get
            {
                if (rotateLimit)
                {
                    var range = angleRange.Length;
                    return (Angle - angleRange.min) / (range == 0 ? 1 : range);
                }
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
            {
                return;
            }

            Angle += GetMouseInput() * rotateSpeed * Time.deltaTime;
            if (rotateLimit)
            {
                Angle = Mathf.Clamp(Angle, angleRange.min, angleRange.max);
            }
            RotateKnob(Angle);

            if (OnSwitchDrag != null)
            {
                OnSwitchDrag.Invoke();
            }
        }

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
            {
                return;
            }

            if (OnSwitchRelease != null)
            {
                OnSwitchRelease.Invoke();
            }

            if (!adsorbent || adsorbableAngles.Length == 0)
            {
                return;
            }

            Angle = GetAdsorbentAngle(Angle, adsorbableAngles);
            RotateKnob(Angle);

            if (OnSwitchAdsorbent != null)
            {
                OnSwitchAdsorbent.Invoke();
            }
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
            {
                return Input.GetAxis("Mouse X");
            }
            return Input.GetAxis("Mouse Y");
        }

        /// <summary>
        /// Get the adsorbent angle base on switch current angle.
        /// </summary>
        /// <param name="currentAngle">Current angle of knob.</param>
        /// <param name="adsorbableAngles">Adsorbable angles of knob.</param>
        /// <returns>Target adsorbent angle of knob.</returns>
        protected float GetAdsorbentAngle(float currentAngle, float[] adsorbableAngles)
        {
            var nearAngle = 0f;
            var deltaAngle = 0f;
            var nearDelta = float.PositiveInfinity;
            foreach (var adsorbentAngle in adsorbableAngles)
            {
                deltaAngle = Mathf.Abs(currentAngle - adsorbentAngle);
                if (deltaAngle < nearDelta)
                {
                    nearDelta = deltaAngle;
                    nearAngle = adsorbentAngle;
                }
            }
            return nearAngle;
        }
        #endregion
    }
}