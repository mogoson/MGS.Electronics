/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ELEKnob.cs
 *  Description  :  Define electronic knob element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Electronics
{
    /// <summary>
    /// Electronic knob element.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ELEKnob : MonoELEElement, IELEKnob
    {
        #region Field and Property
        /// <summary>
        /// Input axis.
        /// </summary>
        [Tooltip("Input axis.")]
        [SerializeField]
        protected string inputAxis = "Mouse X";

        /// <summary>
        /// Knob rotate speed.
        /// </summary>
        [Tooltip("Knob rotate speed.")]
        [SerializeField]
        protected float rotateSpeed = 250;

        /// <summary>
        /// Limit rotate angle?
        /// </summary>
        [Tooltip("Limit rotate angle?")]
        [SerializeField]
        protected bool rotateLimit = false;

        /// <summary>
        /// Range of rotate angle.
        /// </summary>
        [Tooltip("Range of rotate angle.")]
        [SerializeField]
        protected Range angleRange = new Range(-60, 60);

        /// <summary>
        /// Adsorbent to target angle on mouse up?
        /// </summary>
        [Tooltip("Adsorbent to target angle on mouse up?")]
        [SerializeField]
        protected bool adsorbent = false;

        /// <summary>
        /// Adsorbable angles.
        /// </summary>
        [Tooltip("Adsorbable angles.")]
        [SerializeField]
        protected float[] adsorbableAngles;

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 StartAngles { protected set; get; }

        /// <summary>
        /// Input axis.
        /// </summary>
        public string InputAxis
        {
            set { inputAxis = value; }
            get { return inputAxis; }
        }

        /// <summary>
        /// Knob rotate speed.
        /// </summary>
        public float RotateSpeed
        {
            set { rotateSpeed = value; }
            get { return rotateSpeed; }
        }

        /// <summary>
        /// Limit rotate angle?
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
        /// Adsorbent to target angle on mouse up?
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
        /// Knob current angle.
        /// </summary>
        public float Angle { protected set; get; }

        /// <summary>
        /// Knob current rotate percent base range.
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
        /// Knob drag event.
        /// </summary>
        public event Action OnDragEvent
        {
            add { onDragEvent += value; }
            remove { onDragEvent -= value; }
        }
        /// <summary>
        /// Knob drag event.
        /// </summary>
        protected Action onDragEvent;

        /// <summary>
        /// Knob release event.
        /// </summary>
        public event Action OnReleaseEvent
        {
            add { onReleaseEvent += value; }
            remove { onReleaseEvent -= value; }
        }
        /// <summary>
        /// Knob release event.
        /// </summary>
        protected Action onReleaseEvent;

        /// <summary>
        /// Knob adsorbent event.
        /// </summary>
        public event Action OnAdsorbentEvent
        {
            add { onAdsorbentEvent += value; }
            remove { onAdsorbentEvent -= value; }
        }
        /// <summary>
        /// Knob adsorbent event.
        /// </summary>
        protected Action onAdsorbentEvent;
        #endregion

        #region Protected Method
        /// <summary>
        /// Awake component.
        /// </summary>
        protected virtual void Awake()
        {
            StartAngles = transform.localEulerAngles;
        }

        /// <summary>
        /// Response mouse left button drag.
        /// </summary>
        protected virtual void OnMouseDrag()
        {
            if (!isActive)
            {
                return;
            }

            Angle += Input.GetAxis(inputAxis) * rotateSpeed * Time.deltaTime;
            if (rotateLimit)
            {
                Angle = Mathf.Clamp(Angle, angleRange.min, angleRange.max);
            }
            Rotate(Angle);
            if (onDragEvent != null)
            {
                onDragEvent.Invoke();
            }
        }

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isActive)
            {
                return;
            }

            if (onReleaseEvent != null)
            {
                onReleaseEvent.Invoke();
            }

            if (!adsorbent || adsorbableAngles.Length == 0)
            {
                return;
            }

            Angle = GetAdsorbentAngle(Angle, adsorbableAngles);
            Rotate(Angle);
            if (onAdsorbentEvent != null)
            {
                onAdsorbentEvent.Invoke();
            }
        }

        /// <summary>
        /// Rotate knob to target angle.
        /// </summary>
        /// <param name="rotateAngle">Rotate angle.</param>
        protected virtual void Rotate(float rotateAngle)
        {
            transform.localRotation = Quaternion.Euler(StartAngles + Vector3.back * rotateAngle);
        }

        /// <summary>
        /// Get the adsorbent angle base on knob current angle.
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