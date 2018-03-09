/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HelpUI.cs
 *  Description  :  Draw help info in scene.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Handle
{
    [AddComponentMenu("Developer/Handle/HelpUI")]
    public class HelpUI : MonoBehaviour
    {
        #region Field and Property
        [Multiline]
        public string info = "Help info.";

        public float xOfset = 10;
        public float yOfset = 10;
        #endregion

        #region Private Method
        private void OnGUI()
        {
            GUILayout.Space(yOfset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOfset);
            GUILayout.Label(info);
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}