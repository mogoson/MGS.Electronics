/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ELERocker.cs
 *  Description  :  Define electronic rocker element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using UnityEngine;

namespace MGS.Electronics
{
    /// <summary>
    /// Electronic rocker element.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ELERocker : MonoELEElement, IELERocker
    {
        #region Field and Property
        /// <summary>
        /// Radius angle.
        /// </summary>
        [Tooltip("Radius angle.")]
        [SerializeField]
        protected float radiusAngle = 25;

        /// <summary>
        /// Rocker rotate speed.
        /// </summary>
        [Tooltip("Rocker rotate speed.")]
        [SerializeField]
        protected float rotateSpeed = 250;

        /// <summary>
        /// Revert speed.
        /// </summary>
        [Tooltip("Revert speed.")]
        [SerializeField]
        protected float revertSpeed = 0;

        /// <summary>
        /// Current angles.
        /// </summary>
        protected Vector3 angles;

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 StartAngles { protected set; get; }

        /// <summary>
        /// Radius angle.
        /// </summary>
        public float RadiusAngle
        {
            set { radiusAngle = value; }
            get { return radiusAngle; }
        }

        /// <summary>
        /// Rocker rotate speed.
        /// </summary>
        public float RotateSpeed
        {
            set { rotateSpeed = value; }
            get { return rotateSpeed; }
        }

        /// <summary>
        /// Revert speed.
        /// </summary>
        public float RevertSpeed
        {
            set { revertSpeed = value; }
            get { return revertSpeed; }
        }

        /// <summary>
        /// Rocker current angles normalized vector.
        /// </summary>
        public Vector2 Vector
        {
            get { return angles.normalized; }
        }

        /// <summary>
        /// Rocker drag event.
        /// </summary>
        public event Action OnDragEvent
        {
            add { onDragEvent += value; }
            remove { onDragEvent -= value; }
        }
        /// <summary>
        /// Rocker drag event.
        /// </summary>
        protected Action onDragEvent;

        /// <summary>
        /// Rocker Release event.
        /// </summary>
        public event Action OnReleaseEvent
        {
            add { onReleaseEvent += value; }
            remove { onReleaseEvent -= value; }
        }
        /// <summary>
        /// Rocker Release event.
        /// </summary>
        protected Action onReleaseEvent;

        /// <summary>
        /// Rocker revert event.
        /// </summary>
        public event Action OnRevertEvent
        {
            add { onRevertEvent += value; }
            remove { onRevertEvent -= value; }
        }
        /// <summary>
        /// Rocker revert event.
        /// </summary>
        protected Action onRevertEvent;

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerator reverter;
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
        /// Down rocker.
        /// </summary>
        protected virtual void OnMouseDown()
        {
            if (reverter != null)
            {
                StopCoroutine(reverter);
                reverter = null;
            }
        }

        /// <summary>
        /// Drag rocker.
        /// </summary>
        protected virtual void OnMouseDrag()
        {
            if (!isActive)
            {
                return;
            }

            var x = Input.GetAxis("Mouse Y");
            var y = Input.GetAxis("Mouse X");
            angles += new Vector3(x, -y) * rotateSpeed * Time.deltaTime;
            if (angles.magnitude > radiusAngle)
            {
                angles = angles.normalized * radiusAngle;
            }
            Rotate(angles);
            if (onDragEvent != null)
            {
                onDragEvent.Invoke();
            }
        }

        /// <summary>
        /// Release rocker.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isActive)
            {
                return;
            }

            if (revertSpeed > 0)
            {
                if (reverter == null)
                {
                    reverter = Revert();
                }
                StartCoroutine(reverter);
            }
            if (onReleaseEvent != null)
            {
                onReleaseEvent.Invoke();
            }
        }

        /// <summary>
        /// Revert rocker to default.
        /// </summary>
        protected virtual IEnumerator Revert()
        {
            while (angles.magnitude != 0)
            {
                angles = Vector3.MoveTowards(angles, Vector3.zero, revertSpeed * Time.deltaTime);
                Rotate(angles);
                yield return null;
            }
            reverter = null;
            if (onRevertEvent != null)
            {
                onRevertEvent.Invoke();
            }
        }

        /// <summary>
        /// Rotate rocker to target angles.
        /// </summary>
        /// <param name="eulerAngles">Rotate euler angles.</param>
        protected virtual void Rotate(Vector3 eulerAngles)
        {
            transform.localRotation = Quaternion.Euler(StartAngles + eulerAngles);
        }
        #endregion
    }
}