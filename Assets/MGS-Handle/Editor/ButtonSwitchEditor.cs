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
        protected ButtonSwitch Script { get { return target as ButtonSwitch; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = Script.StartPosition;
                    if (Script.transform.parent)
                        point = Script.transform.parent.TransformPoint(point);
                    return point;
                }
                else
                    return Script.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(ZeroPoint, Quaternion.identity, nodeSize);
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
            DrawArrow(ZeroPoint, Script.transform.forward, Script.downOffset, nodeSize, string.Empty, blue);

            if (Script.selfLock)
                DrawSphereCap(ZeroPoint + Script.transform.forward * (Script.downOffset * Script.lockPercent), Quaternion.identity, nodeSize);
        }
        #endregion
    }
}