/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: ButtonSwitch.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/31/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         ButtonSwitch             Ignore.
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
    [AddComponentMenu("Developer/Handle/ButtonSwitch")]
    public class ButtonSwitch : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Is enable to control.
        /// </summary>
        public bool isEnable = true;

        /// <summary>
        /// Switch down offset.
        /// </summary>
        public float downOffset = 1;

        /// <summary>
        /// Self lock on switch down.
        /// </summary>
        public bool selfLock = false;

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        [Range(0, 1)]
        public float lockPercent = 0.5f;

        /// <summary>
        /// High light on switch down.
        /// </summary>
        public bool highLight = false;

        /// <summary>
        /// High light target renderer.
        /// </summary>
        public Renderer lightRender;

        /// <summary>
        /// High light material.
        /// </summary>
        public Material lightMaterial;

        /// <summary>
        /// Button switch is down state.
        /// </summary>
        public bool isDown { protected set; get; }

        /// <summary>
        /// Current offset base start position.
        /// </summary>
        public float currentOffset { protected set; get; }

        /// <summary>
        /// Start position.
        /// </summary>
        public Vector3 startPosition { private set; get; }

        /// <summary>
        /// Local move axis.
        /// </summary>
        protected Vector3 moveAxis
        {
            get
            {
                var forward = transform.forward;
                if (transform.parent)
                    forward = transform.parent.InverseTransformDirection(forward);
                return forward;
            }//get_end
        }//aixs_end

        /// <summary>
        /// Button switch up event.
        /// </summary>
        public HandleEvent switchUpEvent;

        /// <summary>
        /// Button switch down event.
        /// </summary>
        public HandleEvent switchDownEvent;

        /// <summary>
        /// Button switch lock event.
        /// </summary>
        public HandleEvent switchLockEvent;

        /// <summary>
        /// Default material of lightRender.
        /// </summary>
        protected Material defaultMat;

        /// <summary>
        /// Current self lock state.
        /// </summary>
        protected bool isLock;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            startPosition = transform.localPosition;
            if (lightRender)
                defaultMat = lightRender.material;
        }//Awake()_end

        /// <summary>
        /// Response mouse left button down.
        /// </summary>
        protected virtual void OnMouseDown()
        {
            if (!isEnable)
                return;
            isDown = true;
            currentOffset = downOffset;
            TranslateButton(currentOffset);
            if (highLight)
                lightRender.material = lightMaterial;
            if (switchDownEvent != null)
                switchDownEvent();
        }//OnMouseDown()_end

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
                return;
            if (selfLock)
                isLock = !isLock;
            if (isLock)
            {
                currentOffset = downOffset * lockPercent;
                if (switchLockEvent != null)
                    switchLockEvent();
            }
            else
            {
                isDown = false;
                currentOffset = 0;
                if (switchUpEvent != null)
                    switchUpEvent();
            }//if()_end
            TranslateButton(currentOffset);
            if (highLight && !isLock)
                lightRender.material = defaultMat;
        }//OnMouseUp()_end

        /// <summary>
        /// Translate button switch to target position.
        /// </summary>
        /// <param name="offset">Offset of z axis.</param>
        protected virtual void TranslateButton(float offset)
        {
            transform.localPosition = startPosition + moveAxis.normalized * offset;
        }//Trans...()_end
        #endregion
    }//class_end
}//namespace_end