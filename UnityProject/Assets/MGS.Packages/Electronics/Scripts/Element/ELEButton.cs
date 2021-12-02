/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ELEButton.cs
 *  Description  :  Define electronic button component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Electronics
{
    /// <summary>
    /// Electronic button component.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ELEButton : MonoELEElement, IELEButton
    {
        #region Field and Property
        /// <summary>
        /// Button down offset.
        /// </summary>
        [Tooltip("Button down offset.")]
        [SerializeField]
        protected float downOffset = 1;

        /// <summary>
        /// Self lock on button down?
        /// </summary>
        [Tooltip("Self lock on button down?")]
        [SerializeField]
        protected bool selfLock = false;

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        [Tooltip("Self lock offset percent.")]
        [Range(0, 1)]
        [SerializeField]
        protected float lockPercent = 0.5f;

        /// <summary>
        /// Toggle LED on toggle button?
        /// </summary>
        [Tooltip("Toggle LED on toggle button?")]
        [SerializeField]
        protected bool useLED = false;

        /// <summary>
        /// LED of button.
        /// </summary>
        [Tooltip("LED of button.")]
        [SerializeField]
        protected MonoLED monoLED;

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
        /// Button down offset.
        /// </summary>
        public float DownOffset
        {
            set { downOffset = value; }
            get { return downOffset; }
        }

        /// <summary>
        /// Self lock on button down?
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
        /// Toggle LED on toggle button?
        /// </summary>
        public bool UseLED
        {
            set { useLED = value; }
            get { return useLED; }
        }

        /// <summary>
        /// LED of button.
        /// </summary>
        public ILED LED { set; get; }

        /// <summary>
        /// Button is down?
        /// </summary>
        public bool IsDown { protected set; get; }

        /// <summary>
        /// Button up event.
        /// </summary>
        public event Action OnUpEvent
        {
            add { onUpEvent += value; }
            remove { onUpEvent -= value; }
        }
        /// <summary>
        /// Button up event.
        /// </summary>
        protected Action onUpEvent;

        /// <summary>
        /// Button down event.
        /// </summary>
        public event Action OnDownEvent
        {
            add { onDownEvent += value; }
            remove { onDownEvent -= value; }
        }
        /// <summary>
        /// Button down event.
        /// </summary>
        protected Action onDownEvent;

        /// <summary>
        /// Button lock event.
        /// </summary>
        public event Action OnLockEvent
        {
            add { onLockEvent += value; }
            remove { onLockEvent -= value; }
        }
        /// <summary>
        /// Button lock event.
        /// </summary>
        protected Action onLockEvent;
        #endregion

        #region Protected Method
        /// <summary>
        /// Awake component.
        /// </summary>
        protected virtual void Awake()
        {
            StartPosition = transform.localPosition;
            LED = monoLED;
        }

        /// <summary>
        /// Response mouse left button down.
        /// </summary>
        protected virtual void OnMouseDown()
        {
            if (!isActive)
            {
                return;
            }

            IsDown = true;
            currentOffset = downOffset;
            Translate(currentOffset);

            if (useLED)
            {
                LED.TurnOn();
            }

            if (onDownEvent != null)
            {
                onDownEvent.Invoke();
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

            if (selfLock)
            {
                isLock = !isLock;
            }

            if (isLock)
            {
                currentOffset = downOffset * lockPercent;
                if (onLockEvent != null)
                {
                    onLockEvent.Invoke();
                }
            }
            else
            {
                IsDown = false;
                currentOffset = 0;
                if (onUpEvent != null)
                {
                    onUpEvent.Invoke();
                }
            }
            Translate(currentOffset);

            if (useLED && !isLock)
            {
                LED.TurnOff();
            }
        }

        /// <summary>
        /// Translate button to target position.
        /// </summary>
        /// <param name="offset">Offset of z axis.</param>
        protected virtual void Translate(float offset)
        {
            transform.localPosition = StartPosition + MoveAxis * offset;
        }
        #endregion
    }
}