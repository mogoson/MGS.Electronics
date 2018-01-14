/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  KnobSwitchEditor.cs
 *  Description  :  Editor for KnobSwitch.
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
    [CustomEditor(typeof(KnobSwitch), true)]
    [CanEditMultipleObjects]
    public class KnobSwitchEditor : HandleEditor
    {
        #region Property and Field
        protected KnobSwitch Script { get { return target as KnobSwitch; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var up = Quaternion.Euler(Script.StartAngles) * Vector3.up;
                    if (Script.transform.parent)
                        up = Script.transform.parent.rotation * up;
                    return up;
                }
                else
                    return Script.transform.up;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(Script.transform.position, Script.transform.rotation, areaRadius);
            DrawArrow(Script.transform.position, Script.transform.forward, arrowLength, nodeSize, "Axis", blue);
            DrawArrow(Script.transform.position, ZeroAxis, arrowLength, nodeSize, "Zero", blue);
            DrawArrow(Script.transform.position, Script.transform.up, areaRadius, nodeSize, string.Empty, blue);

            Handles.color = transparentBlue;
            if (Script.rangeLimit)
            {
                var fromAxis = Quaternion.AngleAxis(Script.minAngle, Script.transform.forward) * ZeroAxis;
                Handles.DrawSolidArc(Script.transform.position, Script.transform.forward, fromAxis, Script.maxAngle - Script.minAngle, areaRadius);
            }
            else
                Handles.DrawSolidDisc(Script.transform.position, Script.transform.forward, areaRadius);

            if (Script.adsorbent)
            {
                Handles.color = blue;
                foreach (var adsorbent in Script.adsorbentAngles)
                {
                    var adsorbentAxis = Quaternion.AngleAxis(adsorbent, Script.transform.forward) * ZeroAxis;
                    var adsorbentPosition = Script.transform.position + adsorbentAxis.normalized * areaRadius;
                    DrawSphereCap(adsorbentPosition, Quaternion.identity, nodeSize);
                }
            }
        }
        #endregion
    }
}