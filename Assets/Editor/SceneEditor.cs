/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SceneEditor.cs
 *  Description  :  Define scene editor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Electronics.Editors
{
    public class SceneEditor : Editor
    {
#if UNITY_5_5_OR_NEWER
        protected readonly Handles.CapFunction CircleCap = Handles.CircleHandleCap;
        protected readonly Handles.CapFunction SphereCap = Handles.SphereHandleCap;
#else
        protected readonly Handles.DrawCapFunction CircleCap = Handles.CircleCap;
        protected readonly Handles.DrawCapFunction SphereCap = Handles.SphereCap;
#endif
        protected const float NodeSize = 0.125f;
        protected const float AreaRadius = 1.25f;
        protected const float ArrowLength = 2f;

        protected void DrawCircleCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
            {
                CircleCap(0, position, rotation, size, EventType.Repaint);
            }
#else
            CircleCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveCircleCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawCircleCap(position, rotation, size * GetHandleSize(position));
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
            {
                SphereCap(0, position, rotation, size, EventType.Repaint);
            }
#else
            SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveSphereCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawSphereCap(position, rotation, size * GetHandleSize(position));
        }

        protected void DrawAdaptiveWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawWireArc(center, normal, from, angle, radius * GetHandleSize(center));
        }

        protected void DrawAdaptiveSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawSolidArc(center, normal, from, angle, radius * GetHandleSize(center));
        }

        protected void DrawAdaptiveSolidDisc(Vector3 center, Vector3 normal, float radius)
        {
            Handles.DrawSolidDisc(center, normal, radius * GetHandleSize(center));
        }

        protected void DrawSphereArrow(Vector3 start, Vector3 end, float size, string text = "")
        {
            Handles.DrawLine(start, end);
            DrawAdaptiveSphereCap(end, Quaternion.identity, size);
            Handles.Label(end, text);
        }

        protected void DrawSphereArrow(Vector3 start, Vector3 direction, float length, float size, string text = "")
        {
            DrawSphereArrow(start, start + direction.normalized * length, size, text);
        }

        protected void DrawAdaptiveSphereArrow(Vector3 start, Vector3 direction, float length, float size, string text = "")
        {
            DrawSphereArrow(start, direction, length * GetHandleSize(start), size, text);
        }

        protected float GetHandleSize(Vector3 position)
        {
            return HandleUtility.GetHandleSize(position);
        }
    }
}