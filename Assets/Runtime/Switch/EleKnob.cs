/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleKnob.cs
 *  Description  :  Define electronic knob element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Electronics
{
    public enum KnobState
    {
        DRAG,
        RELEASE,
        ADSORBENT
    }

    /// <summary>
    /// Electronic knob element.
    /// </summary>
    [AddComponentMenu("MGS/Electronic/Ele Knob")]
    [RequireComponent(typeof(Collider))]
    public class EleKnob : EleSwitch<KnobState>
    {
        /// <summary>
        /// Input axis.
        /// </summary>
        public string inputAxis = InputAxis.MOUSE_X;

        /// <summary>
        /// Knob rotate speed.
        /// </summary>
        public float rotateSpeed = 500;

        /// <summary>
        /// Limit rotate angle?
        /// </summary>
        public bool rotateLimit;

        /// <summary>
        /// Range of rotate angle.
        /// </summary>
        public Range angleRange = new Range(-60, 60);

        /// <summary>
        /// Adsorbent to target angle on mouse up?
        /// </summary>
        public bool adsorbent;

        /// <summary>
        /// Adsorbable angles.
        /// </summary>
        public float[] adsorbableAngles;

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 OriginAngles { protected set; get; }

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
                    var range = angleRange.Size;
                    return (Angle - angleRange.min) / (range == 0 ? 1 : range);
                }
                return 0;
            }
        }

        /// <summary>
        /// Awake component.
        /// </summary>
        protected virtual void Awake()
        {
            OriginAngles = transform.localEulerAngles;
        }

        /// <summary>
        /// Response mouse left button drag.
        /// </summary>
        protected virtual void OnMouseDrag()
        {
            if (!isInteractable)
            {
                return;
            }

            Angle += Input.GetAxis(inputAxis) * rotateSpeed * Time.deltaTime;
            if (rotateLimit)
            {
                Angle = Mathf.Clamp(Angle, angleRange.min, angleRange.max);
            }

            Rotate(Angle);
            InvokeOnSwitch(KnobState.DRAG);
        }

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isInteractable)
            {
                return;
            }

            InvokeOnSwitch(KnobState.RELEASE);
            if (!adsorbent || adsorbableAngles.Length == 0)
            {
                return;
            }

            Angle = GetAdsorbentAngle(Angle, adsorbableAngles);
            Rotate(Angle);
            InvokeOnSwitch(KnobState.ADSORBENT);
        }

        /// <summary>
        /// Rotate knob to target angle.
        /// </summary>
        /// <param name="rotateAngle">Rotate angle.</param>
        protected virtual void Rotate(float rotateAngle)
        {
            transform.localRotation = Quaternion.Euler(OriginAngles + Vector3.back * rotateAngle);
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
            var nearDelta = float.PositiveInfinity;
            foreach (var adsorbentAngle in adsorbableAngles)
            {
                var deltaAngle = Mathf.Abs(currentAngle - adsorbentAngle);
                if (deltaAngle < nearDelta)
                {
                    nearDelta = deltaAngle;
                    nearAngle = adsorbentAngle;
                }
            }
            return nearAngle;
        }
    }
}