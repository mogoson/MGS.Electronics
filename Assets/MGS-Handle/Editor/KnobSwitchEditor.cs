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

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.Device
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
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawAdaptiveCircleCap(Target.transform.position, Target.transform.rotation, AreaRadius);
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.forward, ArrowLength, NodeSize, "Axis");

            DrawAdaptiveSphereArrow(Target.transform.position, ZeroAxis, ArrowLength, NodeSize, "Zero");
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.up, AreaRadius, NodeSize);

            Handles.color = TransparentBlue;
            if (Target.RotateLimit)
            {
                var fromAxis = Quaternion.AngleAxis(Target.AngleInterval.min, Target.transform.forward) * ZeroAxis;
                DrawAdaptiveSolidArc(Target.transform.position, Target.transform.forward, fromAxis, Target.AngleInterval.max - Target.AngleInterval.min, AreaRadius);
            }
            else
                DrawAdaptiveSolidDisc(Target.transform.position, Target.transform.forward, AreaRadius);

            if (Target.Adsorbent)
            {
                Handles.color = Blue;
                foreach (var adsorbent in Target.AdsorbentAngles)
                {
                    var adsorbentAxis = Quaternion.AngleAxis(adsorbent, Target.transform.forward) * ZeroAxis;
                    var adaptiveScale = HandleUtility.GetHandleSize(Target.transform.position);
                    var adsorbentPosition = Target.transform.position + adsorbentAxis.normalized * AreaRadius * adaptiveScale;
                    DrawAdaptiveSphereCap(adsorbentPosition, Quaternion.identity, NodeSize);
                }
            }
        }
        #endregion
    }
}