/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: HandleEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/1/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         HandleEditor             Ignore.
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

    public class HandleEditor : Editor
    {
        #region Property and Field
        #region Color
        protected Color blue = new Color(0, 1, 1, 1);
        protected Color transparentBlue = new Color(0, 1, 1, 0.1f);
        #endregion

        #region Length
        protected float nodeSize = 0.05f;
        protected float arrowLength = 0.75f;
        protected float areaRadius = 0.5f;
        #endregion
        #endregion

        #region Protected Method
        protected virtual void DrawArrow(Vector3 start, Vector3 direction, float length, float size, string text, Color color)
        {
            var gC = GUI.color;
            var hC = Handles.color;

            GUI.color = color;
            Handles.color = color;

            var end = start + direction.normalized * length;
            Handles.DrawLine(start, end);
            Handles.SphereCap(0, end, Quaternion.identity, size);
            Handles.Label(end, text);

            GUI.color = gC;
            Handles.color = hC;
        }
        #endregion
    }
}