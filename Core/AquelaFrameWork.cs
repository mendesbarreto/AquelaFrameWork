using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using AFFrameWork.Core.Window;

namespace AFFrameWork.Core
{
    public class AquelaFrameWork : Editor
    {
        
        [MenuItem("AquelaFrameWork/SmartFoxServer")]
        static void ConfigSmartFoxServer()
        {
            AFSmartFoxServerWindow.Init();
        }

        [MenuItem("AquelaFrameWork/Settings")]
        static void ProjectSettings()
        {
            AFFoulderStructWindow.Init();
        }
    }
}
