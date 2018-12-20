/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ButtonSwitchEditor.cs
 *  Description  :  Editor for ButtonSwitch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/22/2018
 *  Description  :  Optimize display of node.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.Device
{
    [CustomEditor(typeof(ButtonSwitch), true)]
    [CanEditMultipleObjects]
    public class ButtonSwitchEditor : GenericEditor
    {
        #region Field and Property 
        protected ButtonSwitch Target { get { return target as ButtonSwitch; } }

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
            Handles.color = Blue;
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