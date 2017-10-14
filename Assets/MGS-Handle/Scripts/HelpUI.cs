/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  HelpUI.cs
 *  Description  :  Draw help message in scene.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *************************************************************************/
 
using UnityEngine;

namespace Developer.Handle
{
    [AddComponentMenu("Developer/Handle/HelpUI")]
    public class HelpUI : MonoBehaviour
    {
        #region Property and Field
        [Multiline]
        public string text;
        public float xOfset = 10;
        public float yOfset = 10;
        #endregion

        #region Private Method
        private void OnGUI()
        {
            GUILayout.Space(yOfset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOfset);
            GUILayout.Label(text);
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}