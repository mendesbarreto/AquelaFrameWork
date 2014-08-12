using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using AFFrameWork.Core.Window;

namespace AFFrameWork.Core
{
    public class AqueleFrameWork : Editor
    {
        [MenuItem ("MDSFrameWork/SmartFoxServer")]
        static void ConfigSmartFoxServer()
        {
            AFSmartFoxServerWindow.Init();
        }

        [MenuItem("MDSFrameWork/Settings")]
        static void ProjectSettings()
        {
            AFFoulderStructWindow.Init();
        }

    }
}
