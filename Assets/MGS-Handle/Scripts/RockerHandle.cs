/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *  FileName: RockerHandle.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 4/1/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         RockerHandle             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     4/1/2016       0.1.0        Create this file.
 *************************************************************************/

using UnityEngine;

namespace Developer.Handle
{
    [RequireComponent(typeof(Collider))]
    [AddComponentMenu("Developer/Handle/RockerHandle")]
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
        public Vector2 handleVector { get { return angles.normalized; } }

        /// <summary>
        /// Current angles.
        /// </summary>
        public Vector3 angles { protected set; get; }

        /// <summary>
        /// Start angles.
        /// </summary>
        public Vector3 startAngles { private set; get; }

        /// <summary>
        /// Handle drag event.
        /// </summary>
        public HandleEvent handleDragEvent;

        /// <summary>
        /// Handle Release event.
        /// </summary>
        public HandleEvent handleReleaseEvent;

        /// <summary>
        /// Handle revert event.
        /// </summary>
        public HandleEvent handleRevertEvent;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            startAngles = transform.localEulerAngles;
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
            angles += new Vector3(x, -y) * rotateSpeed * Time.deltaTime;
            if (angles.magnitude > radiusAngle)
                angles = angles.normalized * radiusAngle;
            RotateHandle(angles);
            if (handleDragEvent != null)
                handleDragEvent();
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
            if (handleReleaseEvent != null)
                handleReleaseEvent();
        }

        /// <summary>
        /// Revert handle to default.
        /// </summary>
        protected virtual void RevertHandle()
        {
            if (angles.magnitude == 0)
            {
                CancelInvoke("RevertHandle");
                if (handleRevertEvent != null)
                    handleRevertEvent();
            }//if()_end
            angles = Vector3.MoveTowards(angles, Vector3.zero, revertSpeed * Time.deltaTime);
            RotateHandle(angles);
        }

        /// <summary>
        /// Rotate handle.
        /// </summary>
        /// <param name="eulerAngles">Rotate euler angles.</param>
        protected virtual void RotateHandle(Vector3 eulerAngles)
        {
            var euler = startAngles + eulerAngles;
            transform.localRotation = Quaternion.Euler(euler);
        }
        #endregion
    }
}