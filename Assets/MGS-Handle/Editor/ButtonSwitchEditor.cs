/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  ButtonSwitchEditor.cs
 *  Description  :  Editor for ButtonSwitch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/1/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Handle
{
    [CustomEditor(typeof(ButtonSwitch), true)]
    [CanEditMultipleObjects]
    public class ButtonSwitchEditor : HandleEditor
    {
        #region Property and Field
        protected ButtonSwitch script { get { return target as ButtonSwitch; } }
        protected Vector3 zeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = script.startPosition;
                    if (script.transform.parent)
                        point = script.transform.parent.TransformPoint(point);
                    return point;
                }
                else
                    return script.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(zeroPoint, Quaternion.identity, nodeSize);
            if (script.selfLock)
                DrawSphereCap(zeroPoint + script.transform.forward * (script.downOffset * script.lockPercent), Quaternion.identity, nodeSize);
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            DrawArrow(zeroPoint, script.transform.forward, script.downOffset, nodeSize, string.Empty, blue);
        }
        #endregion
    }
}