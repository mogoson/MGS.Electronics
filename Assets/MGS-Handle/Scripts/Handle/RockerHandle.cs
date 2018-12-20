/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerHandle.cs
 *  Description  :  Define rocker handle.
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
    /// Handle with rocker.
    /// </summary>
    [AddComponentMenu("Mogoson/Device/RockerHandle")]
    [RequireComponent(typeof(Collider))]
    public class RockerHandle : MonoDevice, IRockerHandle
    {
        #region Field and Property 
        /// <summary>
        /// Enable to control.
        /// </summary>
        [SerializeField]
        protected bool isEnable = true;

        /// <summary>
        /// Radius angle.
        /// </summary>
        [SerializeField]
        protected float radiusAngle = 25;

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        [SerializeField]
        protected float rotateSpeed = 250;

        /// <summary>
        /// Revert speed.
        /// </summary>
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
        /// Enable to control.
        /// </summary>
        public override bool IsEnable
        {
            set { isEnable = value; }
            get { return isEnable; }
        }

        /// <summary>
        /// Radius angle.
        /// </summary>
        public float RadiusAngle
        {
            set { radiusAngle = value; }
            get { return radiusAngle; }
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
        /// Revert speed.
        /// </summary>
        public float RevertSpeed
        {
            set { revertSpeed = value; }
            get { return revertSpeed; }
        }

        /// <summary>
        /// Handle out put normalized vector.
        /// </summary>
        public Vector2 Vector
        {
            get { return angles.normalized; }
        }

        /// <summary>
        /// Handle drag event.
        /// </summary>
        public event Action OnHandleDrag;

        /// <summary>
        /// Handle Release event.
        /// </summary>
        public event Action OnHandleRelease;

        /// <summary>
        /// Handle revert event.
        /// </summary>
        public event Action OnHandleRevert;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            StartAngles = transform.localEulerAngles;
        }

        /// <summary>
        /// Drag handle.
        /// </summary>
        protected virtual void OnMouseDrag()
        {
            if (!isEnable)
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
            RotateHandle(angles);

            if (OnHandleDrag != null)
            {
                OnHandleDrag.Invoke();
            }
        }

        /// <summary>
        /// Release handle.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
            {
                return;
            }

            if (revertSpeed > 0)
            {
                InvokeRepeating("RevertHandle", 0, Time.fixedDeltaTime);
            }

            if (OnHandleRelease != null)
            {
                OnHandleRelease.Invoke();
            }
        }

        /// <summary>
        /// Revert handle to default.
        /// </summary>
        protected virtual void RevertHandle()
        {
            if (angles.magnitude == 0)
            {
                CancelInvoke("RevertHandle");

                if (OnHandleRevert != null)
                {
                    OnHandleRevert.Invoke();
                }
            }
            angles = Vector3.MoveTowards(angles, Vector3.zero, revertSpeed * Time.deltaTime);
            RotateHandle(angles);
        }

        /// <summary>
        /// Rotate handle.
        /// </summary>
        /// <param name="eulerAngles">Rotate euler angles.</param>
        protected virtual void RotateHandle(Vector3 eulerAngles)
        {
            transform.localRotation = Quaternion.Euler(StartAngles + eulerAngles);
        }
        #endregion
    }
}