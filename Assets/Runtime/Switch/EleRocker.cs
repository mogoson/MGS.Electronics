/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleRocker.cs
 *  Description  :  Define electronic rocker element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections;
using UnityEngine;

namespace MGS.Electronics
{
    public enum RockerState
    {
        DRAG,
        RELEASE,
        REVERT
    }

    /// <summary>
    /// Electronic rocker element.
    /// </summary>
    [AddComponentMenu("MGS/Electronic/Ele Rocker")]
    [RequireComponent(typeof(Collider))]
    public class EleRocker : EleSwitch<RockerState>
    {
        /// <summary>
        /// Radius angle.
        /// </summary>
        public float radiusAngle = 25;

        /// <summary>
        /// Rocker rotate speed.
        /// </summary>
        public float rotateSpeed = 300;

        /// <summary>
        /// Revert speed.
        /// </summary>
        public float revertSpeed = 300;

        /// <summary>
        /// Current angles.
        /// </summary>
        protected Vector3 angles;

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 OriginAngles { protected set; get; }

        /// <summary>
        /// Reverter coroutine.
        /// </summary>
        protected Coroutine reverter;

        /// <summary>
        /// Awake component.
        /// </summary>
        protected virtual void Awake()
        {
            OriginAngles = transform.localEulerAngles;
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
            if (!isInteractable)
            {
                return;
            }

            var x = Input.GetAxis(InputAxis.MOUSE_Y);
            var y = Input.GetAxis(InputAxis.MOUSE_X);
            angles += rotateSpeed * Time.deltaTime * new Vector3(x, -y);
            if (angles.magnitude > radiusAngle)
            {
                angles = angles.normalized * radiusAngle;
            }
            Rotate(angles);
            InvokeOnSwitch(RockerState.DRAG);
        }

        /// <summary>
        /// Release rocker.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isInteractable)
            {
                return;
            }

            if (revertSpeed > 0)
            {
                reverter = StartCoroutine(Revert());
            }
            InvokeOnSwitch(RockerState.RELEASE);
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
            InvokeOnSwitch(RockerState.REVERT);
        }

        /// <summary>
        /// Rotate rocker to target angles.
        /// </summary>
        /// <param name="eulerAngles">Rotate euler angles.</param>
        protected virtual void Rotate(Vector3 eulerAngles)
        {
            transform.localRotation = Quaternion.Euler(OriginAngles + eulerAngles);
        }
    }
}