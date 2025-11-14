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

using MGS.Editors;
using UnityEngine;

namespace MGS.Electronics.Editors
{
    public class EleEditor : SceneEditor
    {
        protected const float AreaRadius = 1.25f;
        protected const float ArrowLength = 2f;

        protected readonly Color HandleColor = Color.white;
        protected readonly Color AreaColor = new Color(1, 1, 1, 0.1f);
    }
}