/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: HelpUI.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/31/2016
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
 *     1.     Mogoson     3/31/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Handle
{
    using UnityEngine;

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
        //Draw the help text.
        void OnGUI()
        {
            GUILayout.Space(yOfset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOfset);
            GUILayout.Label(text);
            GUILayout.EndHorizontal();
        }//OnGUI()_end
        #endregion
    }//class_end
}//namespace_end