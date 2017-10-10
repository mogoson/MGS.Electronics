/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *  FileName: HelpUI.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 3/31/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.            HelpUI                Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/31/2016       0.1.0        Create this file.
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