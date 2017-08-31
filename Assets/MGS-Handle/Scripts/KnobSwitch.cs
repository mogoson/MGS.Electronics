/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: KnobSwitch.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/31/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         KnobSwitch               Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/31/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Handle
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    [AddComponentMenu("Developer/Handle/KnobSwitch")]
    public class KnobSwitch : MonoBehaviour
    {
        #region Property and Field
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
        public float angle { protected set; get; }

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 startAngles { private set; get; }

        /// <summary>
        /// Switch current rotate percent base range.
        /// </summary>
        public float percent
        {
            get
            {
                if (rangeLimit)
                {
                    var range = maxAngle - minAngle;
                    return (angle - minAngle) / (range == 0 ? 1 : range);
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// Knob switch drag event.
        /// </summary>
        public HandleEvent switchDragEvent;

        /// <summary>
        /// Knob switch release event.
        /// </summary>
        public HandleEvent switchReleaseEvent;

        /// <summary>
        /// Knob switch adsorbent event.
        /// </summary>
        public HandleEvent switchAdsorbentEvent;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            startAngles = transform.localEulerAngles;
        }

        /// <summary>
        /// Response mouse left button drag.
        /// </summary>
        protected virtual void OnMouseDrag()
        {
            if (!isEnable)
                return;
            angle += GetMouseInput() * rotateSpeed * Time.deltaTime;
            if (rangeLimit)
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
            RotateKnob(angle);
            if (switchDragEvent != null)
                switchDragEvent();
        }

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
                return;
            if (switchReleaseEvent != null)
                switchReleaseEvent();
            if (!adsorbent || adsorbentAngles.Length == 0)
                return;
            var nearAngle = 0f;
            var tempNear = float.PositiveInfinity;
            foreach (var adsorbentAngle in adsorbentAngles)
            {
                var deltaAngle = Mathf.Abs(angle - adsorbentAngle);
                if (deltaAngle < tempNear)
                {
                    tempNear = deltaAngle;
                    nearAngle = adsorbentAngle;
                }
            }
            angle = nearAngle;
            RotateKnob(angle);
            if (switchAdsorbentEvent != null)
                switchAdsorbentEvent();
        }

        /// <summary>
        /// Rotate knob switch to target angle.
        /// </summary>
        /// <param name="rotateAngle">Rotate angle.</param>
        protected virtual void RotateKnob(float rotateAngle)
        {
            var euler = startAngles + Vector3.back * rotateAngle;
            transform.localRotation = Quaternion.Euler(euler);
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