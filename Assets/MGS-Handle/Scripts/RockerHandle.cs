/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerHandle.cs
 *  Description  :  Define rocker handle.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/1/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Handle
{
    [AddComponentMenu("Developer/Handle/RockerHandle")]
    [RequireComponent(typeof(Collider))]
    public class RockerHandle : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Enable control.
        /// </summary>
        public bool isEnable = true;

        /// <summary>
        /// Radius angle.
        /// </summary>
        public float radiusAngle = 25;

        /// <summary>
        /// Switch rotate speed.
        /// </summary>
        public float rotateSpeed = 250;

        /// <summary>
        /// Revert speed.
        /// </summary>
        public float revertSpeed = 0;

        /// <summary>
        /// Handle out put normalized vector.
        /// </summary>
        public Vector2 HandleVector { get { return Angles.normalized; } }

        /// <summary>
        /// Current angles.
        /// </summary>
        public Vector3 Angles { protected set; get; }

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 StartAngles { private set; get; }

        /// <summary>
        /// Handle drag event.
        /// </summary>
        public HandleEvent OnHandleDrag;

        /// <summary>
        /// Handle Release event.
        /// </summary>
        public HandleEvent OnHandleRelease;

        /// <summary>
        /// Handle revert event.
        /// </summary>
        public HandleEvent OnHandleRevert;
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
                return;

            var x = Input.GetAxis("Mouse Y");
            var y = Input.GetAxis("Mouse X");
            Angles += new Vector3(x, -y) * rotateSpeed * Time.deltaTime;
            if (Angles.magnitude > radiusAngle)
                Angles = Angles.normalized * radiusAngle;
            RotateHandle(Angles);

            if (OnHandleDrag != null)
                OnHandleDrag();
        }

        /// <summary>
        /// Release handle.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
                return;

            if (revertSpeed > 0)
                InvokeRepeating("RevertHandle", 0, Time.fixedDeltaTime);

            if (OnHandleRelease != null)
                OnHandleRelease();
        }

        /// <summary>
        /// Revert handle to default.
        /// </summary>
        protected virtual void RevertHandle()
        {
            if (Angles.magnitude == 0)
            {
                CancelInvoke("RevertHandle");

                if (OnHandleRevert != null)
                    OnHandleRevert();
            }
            Angles = Vector3.MoveTowards(Angles, Vector3.zero, revertSpeed * Time.deltaTime);
            RotateHandle(Angles);
        }

        /// <summary>
        /// Rotate handle.
        /// </summary>
        /// <param name="eulerAngles">Rotate euler angles.</param>
        protected virtual void RotateHandle(Vector3 eulerAngles)
        {
            var euler = StartAngles + eulerAngles;
            transform.localRotation = Quaternion.Euler(euler);
        }
        #endregion
    }
}