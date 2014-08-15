using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using AFFrameWork.Core.Window;

namespace AFFrameWork.Core
{
    public class AqueleFrameWork : Editor
    {
        [MenuItem("AqueleFrameWork/SmartFoxServer")]
        static void ConfigSmartFoxServer()
        {
            AFSmartFoxServerWindow.Init();
        }

        [MenuItem("AqueleFrameWork/Settings")]
        static void ProjectSettings()
        {
            AFFoulderStructWindow.Init();
        }

    }
}
