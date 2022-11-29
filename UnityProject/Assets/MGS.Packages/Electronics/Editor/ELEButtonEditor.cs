/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ELEButtonEditor.cs
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
    [CustomEditor(typeof(ELEButton), true)]
    [CanEditMultipleObjects]
    public class ELEButtonEditor : ELEEditor
    {
        #region Field and Property 
        protected ELEButton Target { get { return target as ELEButton; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = Target.StartPosition;
                    if (Target.transform.parent)
                    {
                        point = Target.transform.parent.TransformPoint(point);
                    }
                    return point;
                }
                return Target.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = HandleColor;
            DrawAdaptiveSphereCap(ZeroPoint, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawSphereArrow(ZeroPoint, Target.transform.forward, Target.DownOffset, NodeSize);

            if (Target.SelfLock)
            {
                DrawAdaptiveSphereCap(ZeroPoint + Target.transform.forward * Target.DownOffset * Target.LockPercent, Quaternion.identity, NodeSize);
            }
        }
        #endregion
    }
}