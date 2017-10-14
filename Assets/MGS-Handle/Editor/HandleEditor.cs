/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  HandleEditor.cs
 *  Description  :  Editor for Handle.
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
            DrawSphereCap(end, Quaternion.identity, size);
            Handles.Label(end, text);

            GUI.color = gC;
            Handles.color = hC;
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            Handles.SphereHandleCap(0, position, rotation, size, EventType.Ignore);
#else
            Handles.SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawCircleCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            Handles.CircleHandleCap(0, position, rotation, size, EventType.Ignore);
#else
            Handles.CircleCap(0, position, rotation, size);
#endif
        }
        #endregion
    }
}