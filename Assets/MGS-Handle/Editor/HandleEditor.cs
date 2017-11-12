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
        protected readonly Color blue = new Color(0, 1, 1, 1);
        protected readonly Color transparentBlue = new Color(0, 1, 1, 0.1f);

        protected const float nodeSize = 0.05f;
        protected const float arrowLength = 0.75f;
        protected const float areaRadius = 0.5f;
        #endregion

        #region Protected Method
        protected virtual void DrawArrow(Vector3 start, Vector3 direction, float length, float size, string text, Color color)
        {
            var gColor = GUI.color;
            var hColor = Handles.color;

            GUI.color = color;
            Handles.color = color;

            var end = start + direction.normalized * length;
            Handles.DrawLine(start, end);
            DrawSphereCap(end, Quaternion.identity, size);
            Handles.Label(end, text);

            GUI.color = gColor;
            Handles.color = hColor;
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                Handles.SphereHandleCap(0, position, rotation, size, EventType.Repaint);
#else
            Handles.SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawCircleCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                Handles.CircleHandleCap(0, position, rotation, size, EventType.Repaint);
#else
            Handles.CircleCap(0, position, rotation, size);
#endif
        }
        #endregion
    }
}