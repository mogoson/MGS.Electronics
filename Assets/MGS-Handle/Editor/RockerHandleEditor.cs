/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerHandleEditor.cs
 *  Description  :  Editor for RockerHandle.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/22/2018
 *  Description  :  Optimize display of node and area.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.Handle
{
    [CustomEditor(typeof(RockerHandle), true)]
    [CanEditMultipleObjects]
    public class RockerHandleEditor : GenericEditor
    {
        #region Field and Property 
        protected RockerHandle Target { get { return target as RockerHandle; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var back = Quaternion.Euler(Target.StartAngles) * Vector3.back;
                    if (Target.transform.parent)
                        back = Target.transform.parent.rotation * back;
                    return back;
                }
                else
                    return -Target.transform.forward;
            }
        }

        protected Vector3 CrossAxis { get { return Vector3.Cross(ZeroAxis, new Vector3(ZeroAxis.z, ZeroAxis.y, ZeroAxis.x)); } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereArrow(Target.transform.position, -Target.transform.forward, ArrowLength, NodeSize);

            var fromAxis = Quaternion.AngleAxis(Target.radiusAngle, CrossAxis) * ZeroAxis;
            DrawAdaptiveWireArc(Target.transform.position, ZeroAxis, fromAxis, 360, AreaRadius);

            Handles.color = TransparentBlue;
            DrawAdaptiveSolidArc(Target.transform.position, ZeroAxis, fromAxis, 360, AreaRadius);
        }
        #endregion
    }
}