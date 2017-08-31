/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: KnobSwitchEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/1/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        KnobSwitchEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     4/1/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Handle
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(KnobSwitch), true)]
    [CanEditMultipleObjects]
    public class KnobSwitchEditor : HandleEditor
    {
        #region Property and Field
        protected KnobSwitch script { get { return target as KnobSwitch; } }
        protected Vector3 zeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var up = Quaternion.Euler(script.startAngles) * Vector3.up;
                    if (script.transform.parent)
                        up = script.transform.parent.rotation * up;
                    return up;
                }
                else
                    return script.transform.up;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.CircleCap(0, script.transform.position, script.transform.rotation, areaRadius);
            DrawArrow(script.transform.position, script.transform.forward, arrowLength, nodeSize, "Axis", blue);
            DrawArrow(script.transform.position, zeroAxis, arrowLength, nodeSize, "Zero", blue);
            DrawArrow(script.transform.position, script.transform.up, areaRadius, nodeSize, string.Empty, blue);
            Handles.color = transparentBlue;
            if (script.rangeLimit)
            {
                var fromAxis = Quaternion.AngleAxis(script.minAngle, script.transform.forward) * zeroAxis;
                Handles.DrawSolidArc(script.transform.position, script.transform.forward, fromAxis, script.maxAngle - script.minAngle, areaRadius);
            }
            else
                Handles.DrawSolidDisc(script.transform.position, script.transform.forward, areaRadius);
            if (script.adsorbent)
            {
                Handles.color = blue;
                foreach (var adsorbent in script.adsorbentAngles)
                {
                    var adsorbentAxis = Quaternion.AngleAxis(adsorbent, script.transform.forward) * zeroAxis;
                    var adsorbentPosition = script.transform.position + adsorbentAxis.normalized * areaRadius;
                    Handles.SphereCap(0, adsorbentPosition, Quaternion.identity, nodeSize);
                }
            }
        }
        #endregion
    }
}