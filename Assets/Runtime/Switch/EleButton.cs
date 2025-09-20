/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleButton.cs
 *  Description  :  Define electronic button component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Electronics
{
    public enum BtnState
    {
        DOWN,
        LOCK,
        UP
    }

    /// <summary>
    /// Electronic button component.
    /// </summary>
    [AddComponentMenu("MGS/Electronic/Ele Button")]
    [RequireComponent(typeof(Collider))]
    public class EleButton : EleSwitch<BtnState>
    {
        /// <summary>
        /// Button down offset.
        /// </summary>
        public float downOffset = 1;

        /// <summary>
        /// Self lock on button down?
        /// </summary>
        public bool selfLock;

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        [Range(0, 1)]
        public float lockPercent = 0.5f;

        /// <summary>
        /// EleLight of button.
        /// </summary>
        public EleLight eleLight;

        /// <summary>
        /// Button is down?
        /// </summary>
        public bool IsDown { protected set; get; }

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
        /// Origin position.
        /// </summary>
        public Vector3 OriginPos { protected set; get; }

        /// <summary>
        /// Current self lock state.
        /// </summary>
        protected bool isLock;

        /// <summary>
        /// Awake component.
        /// </summary>
        protected virtual void Awake()
        {
            OriginPos = transform.localPosition;
        }

        /// <summary>
        /// Response mouse left button down.
        /// </summary>
        protected virtual void OnMouseDown()
        {
            if (!isInteractable)
            {
                return;
            }

            IsDown = true;
            Translate(downOffset);

            if (eleLight != null)
            {
                eleLight.IsOn = true;
            }
            InvokeOnSwitch(BtnState.DOWN);
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

            if (selfLock)
            {
                isLock = !isLock;
            }

            var offset = 0f;
            if (isLock)
            {
                offset = downOffset * lockPercent;
                InvokeOnSwitch(BtnState.LOCK);
            }
            else
            {
                IsDown = false;
                InvokeOnSwitch(BtnState.UP);
            }

            Translate(offset);
            if (eleLight != null && !isLock)
            {
                eleLight.IsOn = false;
            }
        }

        /// <summary>
        /// Translate button to target position.
        /// </summary>
        /// <param name="offset">Offset of z axis.</param>
        protected virtual void Translate(float offset)
        {
            transform.localPosition = OriginPos + MoveAxis * offset;
        }
    }
}