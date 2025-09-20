/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleButtonEditor.cs
 *  Description  :  Editor for electronic button element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Electronics.Editors
{
    [CustomEditor(typeof(EleButton), true)]
    [CanEditMultipleObjects]
    public class EleButtonEditor : EleEditor
    {
        protected EleButton Target { get { return target as EleButton; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = Target.OriginPos;
                    if (Target.transform.parent)
                    {
                        point = Target.transform.parent.TransformPoint(point);
                    }
                    return point;
                }
                return Target.transform.position;
            }
        }

        protected virtual void OnSceneGUI()
        {
            Handles.color = HandleColor;
            DrawAdaptiveSphereCap(ZeroPoint, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawSphereArrow(ZeroPoint, Target.transform.forward, Target.downOffset, NodeSize);

            if (Target.selfLock)
            {
                DrawAdaptiveSphereCap(ZeroPoint + Target.transform.forward * Target.downOffset * Target.lockPercent, Quaternion.identity, NodeSize);
            }
        }
    }
}