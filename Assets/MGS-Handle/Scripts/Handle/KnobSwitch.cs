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

using Mogoson.Mathematics;
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
        /// Interval of rotate angle.
        /// </summary>
        [SerializeField]
        protected Interval angleInterval = new Interval(-60, 60);

        /// <summary>
        /// Adsorbent to target angle on mouse up.
        /// </summary>
        [SerializeField]
        protected bool adsorbent = false;

        /// <summary>
        /// Target adsorbent angles.
        /// </summary>
        [SerializeField]
        protected float[] adsorbentAngles;

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
        /// Interval of rotate angle.
        /// </summary>
        public Interval AngleInterval
        {
            set { angleInterval = value; }
            get { return angleInterval; }
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
        /// Target adsorbent angles.
        /// </summary>
        public float[] AdsorbentAngles
        {
            set { adsorbentAngles = value; }
            get { return adsorbentAngles; }
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
                    var range = angleInterval.Length;
                    return (Angle - angleInterval.min) / (range == 0 ? 1 : range);
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
            if (rotateLimit)
                Angle = Mathf.Clamp(Angle, angleInterval.min, angleInterval.max);
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