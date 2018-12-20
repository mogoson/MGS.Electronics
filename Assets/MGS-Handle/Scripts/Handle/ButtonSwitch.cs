/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ButtonSwitch.cs
 *  Description  :  Define button switch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  3/9/2018
 *  Description  :  Use HandleLED to control the LED of button switch.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Device
{
    /// <summary>
    /// Switch with button.
    /// </summary>
    [AddComponentMenu("Mogoson/Device/ButtonSwitch")]
    [RequireComponent(typeof(Collider))]
    public class ButtonSwitch : MonoDevice, IButtonSwitch
    {
        #region Field and Property 
        /// <summary>
        /// Is enable to control.
        /// </summary>
        [SerializeField]
        protected bool isEnable = true;

        /// <summary>
        /// Switch down offset.
        /// </summary>
        [SerializeField]
        protected float downOffset = 1;

        /// <summary>
        /// Self lock on switch down.
        /// </summary>
        [SerializeField]
        protected bool selfLock = false;

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        [Range(0, 1)]
        [SerializeField]
        protected float lockPercent = 0.5f;

        /// <summary>
        /// Toggle LED on toggle button.
        /// </summary>
        [SerializeField]
        protected bool useLED = false;

        /// <summary>
        /// LED of button switch.
        /// </summary>
        [SerializeField]
        protected MonoLED led;

        /// <summary>
        /// Current offset base start position.
        /// </summary>
        protected float currentOffset;

        /// <summary>
        /// Current self lock state.
        /// </summary>
        protected bool isLock;

        /// <summary>
        /// Local move axis.
        /// </summary>
        protected Vector3 MoveAxis
        {
            get
            {
                var axis = transform.forward;
                if (transform.parent)
                {
                    axis = transform.parent.InverseTransformDirection(axis);
                }
                return axis;
            }
        }

        /// <summary>
        /// Start position.
        /// </summary>
        public Vector3 StartPosition { protected set; get; }

        /// <summary>
        /// Is enable to control.
        /// </summary>
        public override bool IsEnable
        {
            set { isEnable = value; }
            get { return isEnable; }
        }

        /// <summary>
        /// Switch down offset.
        /// </summary>
        public float DownOffset
        {
            set { downOffset = value; }
            get { return downOffset; }
        }

        /// <summary>
        /// Self lock on switch down.
        /// </summary>
        public bool SelfLock
        {
            set { selfLock = value; }
            get { return selfLock; }
        }

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        public float LockPercent
        {
            set { lockPercent = value; }
            get { return lockPercent; }
        }

        /// <summary>
        /// Toggle LED on toggle button.
        /// </summary>
        public bool UseLED
        {
            set { useLED = value; }
            get { return useLED; }
        }

        /// <summary>
        /// LED of button switch.
        /// </summary>
        public ILED LED
        {
            set
            {
                if (value is MonoLED)
                {
                    led = value as MonoLED;
                }
            }
            get { return led; }
        }

        /// <summary>
        /// Button switch is down state.
        /// </summary>
        public bool IsDown { protected set; get; }

        /// <summary>
        /// Button switch up event.
        /// </summary>
        public event Action OnSwitchUp;

        /// <summary>
        /// Button switch down event.
        /// </summary>
        public event Action OnSwitchDown;

        /// <summary>
        /// Button switch lock event.
        /// </summary>
        public event Action OnSwitchLock;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            StartPosition = transform.localPosition;
        }

        /// <summary>
        /// Response mouse left button down.
        /// </summary>
        protected virtual void OnMouseDown()
        {
            if (!isEnable)
            {
                return;
            }

            IsDown = true;
            currentOffset = downOffset;
            TranslateButton(currentOffset);

            if (useLED)
            {
                led.Open();
            }

            if (OnSwitchDown != null)
            {
                OnSwitchDown.Invoke();
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

            if (selfLock)
            {
                isLock = !isLock;
            }

            if (isLock)
            {
                currentOffset = downOffset * lockPercent;

                if (OnSwitchLock != null)
                {
                    OnSwitchLock.Invoke();
                }
            }
            else
            {
                IsDown = false;
                currentOffset = 0;

                if (OnSwitchUp != null)
                {
                    OnSwitchUp.Invoke();
                }
            }
            TranslateButton(currentOffset);

            if (useLED && !isLock)
            {
                led.Close();
            }
        }

        /// <summary>
        /// Translate button switch to target position.
        /// </summary>
        /// <param name="offset">Offset of z axis.</param>
        protected virtual void TranslateButton(float offset)
        {
            transform.localPosition = StartPosition + MoveAxis * offset;
        }
        #endregion
    }
}