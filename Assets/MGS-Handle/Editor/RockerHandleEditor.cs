/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerHandleEditor.cs
 *  Description  :  Editor for RockerHandle.
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
    [CustomEditor(typeof(RockerHandle), true)]
    [CanEditMultipleObjects]
    public class RockerHandleEditor : HandleEditor
    {
        #region Property and Field
        protected RockerHandle Script { get { return target as RockerHandle; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var back = Quaternion.Euler(Script.StartAngles) * Vector3.back;
                    if (Script.transform.parent)
                        back = Script.transform.parent.rotation * back;
                    return back;
                }
                else
                    return -Script.transform.forward;
            }
        }

        protected Vector3 CrossAxis { get { return Vector3.Cross(ZeroAxis, new Vector3(ZeroAxis.z, ZeroAxis.y, ZeroAxis.x)); } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
            DrawArrow(Script.transform.position, -Script.transform.forward, arrowLength, nodeSize, string.Empty, blue);

            var fromAxis = Quaternion.AngleAxis(Script.radiusAngle, CrossAxis) * ZeroAxis;
            Handles.DrawWireArc(Script.transform.position, ZeroAxis, fromAxis, 360, areaRadius);

            Handles.color = transparentBlue;
            Handles.DrawSolidArc(Script.transform.position, ZeroAxis, fromAxis, 360, areaRadius);
        }
        #endregion
    }
}