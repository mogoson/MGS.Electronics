/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ELERockerEditor.cs
 *  Description  :  Editor for electronic rocker element.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Common.Editors;
using UnityEditor;
using UnityEngine;

namespace MGS.Electronics.Editors
{
    [CustomEditor(typeof(ELERocker), true)]
    [CanEditMultipleObjects]
    public class ELERockerEditor : SceneEditor
    {
        #region Field and Property 
        protected ELERocker Target { get { return target as ELERocker; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var axis = Quaternion.Euler(Target.StartAngles) * Vector3.back;
                    if (Target.transform.parent)
                    {
                        axis = Target.transform.parent.rotation * axis;
                    }
                    return axis;
                }
                return -Target.transform.forward;
            }
        }

        protected Vector3 CrossAxis { get { return Vector3.Cross(ZeroAxis, new Vector3(ZeroAxis.z, ZeroAxis.y, ZeroAxis.x)); } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Color.white;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereArrow(Target.transform.position, -Target.transform.forward, ArrowLength, NodeSize);

            var fromAxis = Quaternion.AngleAxis(Target.RadiusAngle, CrossAxis) * ZeroAxis;
            DrawAdaptiveWireArc(Target.transform.position, ZeroAxis, fromAxis, 360, AreaRadius);

            Handles.color = TransparentWhite;
            DrawAdaptiveSolidArc(Target.transform.position, ZeroAxis, fromAxis, 360, AreaRadius);
        }
        #endregion
    }
}