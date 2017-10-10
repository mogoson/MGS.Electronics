/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *  FileName: RockerHandleEditor.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 4/1/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      RockerHandleEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     4/1/2016       0.1.0        Create this file.
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
        protected RockerHandle script { get { return target as RockerHandle; } }
        protected Vector3 zeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var back = Quaternion.Euler(script.startAngles) * Vector3.back;
                    if (script.transform.parent)
                        back = script.transform.parent.rotation * back;
                    return back;
                }
                else
                    return -script.transform.forward;
            }
        }
        protected Vector3 crossAxis { get { return Vector3.Cross(zeroAxis, new Vector3(zeroAxis.z, zeroAxis.y, zeroAxis.x)); } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            var fromAxis = Quaternion.AngleAxis(script.radiusAngle, crossAxis) * zeroAxis;
            Handles.color = blue;
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            Handles.DrawWireArc(script.transform.position, zeroAxis, fromAxis, 360, areaRadius);
            DrawArrow(script.transform.position, -script.transform.forward, arrowLength, nodeSize, string.Empty, blue);
            Handles.color = transparentBlue;
            Handles.DrawSolidArc(script.transform.position, zeroAxis, fromAxis, 360, areaRadius);
        }
        #endregion
    }
}