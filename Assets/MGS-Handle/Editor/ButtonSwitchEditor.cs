/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: ButtonSwitchEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/1/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      ButtonSwitchEditor          Ignore.
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
            Handles.SphereCap(0, zeroPoint, Quaternion.identity, nodeSize);
            if (script.selfLock)
                Handles.SphereCap(0, zeroPoint + script.transform.forward * (script.downOffset * script.lockPercent), Quaternion.identity, nodeSize);
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            DrawArrow(zeroPoint, script.transform.forward, script.downOffset, nodeSize, string.Empty, blue);
        }
        #endregion
    }
}