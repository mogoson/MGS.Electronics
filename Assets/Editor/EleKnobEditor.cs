/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleKnobEditor.cs
 *  DeTargetion  :  Editor for electronic knob element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Electronics.Editors
{
    [CustomEditor(typeof(EleKnob), true)]
    [CanEditMultipleObjects]
    public class EleKnobEditor : EleEditor
    {
        protected EleKnob Target { get { return target as EleKnob; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var axis = Quaternion.Euler(Target.OriginAngles) * Vector3.up;
                    if (Target.transform.parent)
                    {
                        axis = Target.transform.parent.rotation * axis;
                    }
                    return axis;
                }
                return Target.transform.up;
            }
        }

        protected virtual void OnSceneGUI()
        {
            Handles.color = HandleColor;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawAdaptiveCircleCap(Target.transform.position, Target.transform.rotation, AreaRadius);
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.forward, ArrowLength, NodeSize, "Axis");

            DrawAdaptiveSphereArrow(Target.transform.position, ZeroAxis, ArrowLength, NodeSize, "Zero");
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.up, AreaRadius, NodeSize);

            Handles.color = AreaColor;
            if (Target.rotateLimit)
            {
                var fromAxis = Quaternion.AngleAxis(Target.angleRange.min, Target.transform.forward) * ZeroAxis;
                DrawAdaptiveSolidArc(Target.transform.position, Target.transform.forward, fromAxis, Target.angleRange.Size, AreaRadius);
            }
            else
            {
                DrawAdaptiveSolidDisc(Target.transform.position, Target.transform.forward, AreaRadius);
            }

            if (Target.adsorbent)
            {
                Handles.color = HandleColor;
                foreach (var adsorbent in Target.adsorbableAngles)
                {
                    var adsorbentAxis = Quaternion.AngleAxis(adsorbent, Target.transform.forward) * ZeroAxis;
                    var adaptiveScale = HandleUtility.GetHandleSize(Target.transform.position);
                    var adsorbentPosition = Target.transform.position + adsorbentAxis.normalized * AreaRadius * adaptiveScale;
                    DrawAdaptiveSphereCap(adsorbentPosition, Quaternion.identity, NodeSize);
                }
            }
        }
    }
}