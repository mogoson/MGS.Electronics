/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EleEditor.cs
 *  DeTargetion  :  
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  11/29/2022
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Electronics.Editors
{
    public class EleEditor : SceneEditor
    {
        protected readonly Color HandleColor = Color.white;
        protected readonly Color AreaColor = new Color(1, 1, 1, 0.1f);
    }
}