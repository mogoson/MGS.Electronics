/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  KnobSwitchEditor.cs
 *  DeTargetion  :  Editor for KnobSwitch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using Developer.EditorExtension;
using UnityEditor;
using UnityEngine;

namespace Developer.Handle
{
    [CustomEditor(typeof(KnobSwitch), true)]
    [CanEditMultipleObjects]
    public class KnobSwitchEditor : GenericEditor
    {
        #region Field and Property 
        protected KnobSwitch Target { get { return target as KnobSwitch; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var up = Quaternion.Euler(Target.StartAngles) * Vector3.up;
                    if (Target.transform.parent)
                        up = Target.transform.parent.rotation * up;
                    return up;
                }
                else
                    return Target.transform.up;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, AreaRadius);
            DrawSphereArrow(Target.transform.position, Target.transform.forward, ArrowLength, NodeSize, Blue, "Axis");
            DrawSphereArrow(Target.transform.position, ZeroAxis, ArrowLength, NodeSize, Blue, "Zero");
            DrawSphereArrow(Target.transform.position, Target.transform.up, AreaRadius, NodeSize, Blue, string.Empty);

            Handles.color = TransparentBlue;
            if (Target.rangeLimit)
            {
                var fromAxis = Quaternion.AngleAxis(Target.minAngle, Target.transform.forward) * ZeroAxis;
                Handles.DrawSolidArc(Target.transform.position, Target.transform.forward, fromAxis, Target.maxAngle - Target.minAngle, AreaRadius);
            }
            else
                Handles.DrawSolidDisc(Target.transform.position, Target.transform.forward, AreaRadius);

            if (Target.adsorbent)
            {
                Handles.color = Blue;
                foreach (var adsorbent in Target.adsorbentAngles)
                {
                    var adsorbentAxis = Quaternion.AngleAxis(adsorbent, Target.transform.forward) * ZeroAxis;
                    var adsorbentPosition = Target.transform.position + adsorbentAxis.normalized * AreaRadius;
                    DrawSphereCap(adsorbentPosition, Quaternion.identity, NodeSize);
                }
            }
        }
        #endregion
    }
}