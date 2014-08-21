using System;
using System.Diagnostics;
using System.IO;

using UnityEditor;
using UnityEngine;

using AFFrameWork.Core.Assets;

namespace AFFrameWork.Core.Window
{

    
    public class AFFoulderStructWindow : EditorWindow
    {
        static public AFFoulderStructWindow window;

        public const string DIRECTORY_NAME_ASSETS = "Assets";
        public const string DIRECTORY_NAME_LARGE = "Large";
        public const string DIRECTORY_NAME_SMALL = "Small";
        public const string DIRECTORY_NAME_MEDIUM = "Medium";
        public const string DIRECTORY_NAME_SOUND = "Sounds";
        public const string DIRECTORY_NAME_DATA = "Data";
        public const string DIRECTORY_NAME_SCRIPTS = "Scripts";

        [MenuItem("Window/Settings")]
        public static void Init()
        {
            // Get existing open window or if none, make a new one:
            window = (AFFoulderStructWindow)EditorWindow.GetWindow(typeof(AFFoulderStructWindow));
        }

        void OnGUI()
        {
            GUILayout.Label("Package ");
            AFAssetManager.package = EditorGUILayout.TextField(AFAssetManager.package);

            GUILayout.Label("Iphone Path ");
            AFAssetManager.iphonePath = EditorGUILayout.TextField(AFAssetManager.iphonePath);

            GUILayout.Label("Android Path");
            AFAssetManager.androidPath = EditorGUILayout.TextField(AFAssetManager.androidPath);

            GUILayout.Label("Windows Phone 8 ");
            AFAssetManager.windowsPhone8Path = EditorGUILayout.TextField(AFAssetManager.windowsPhone8Path);

            GUILayout.BeginArea( new Rect( (window.position.width * 0.5f) - 100 , (window.position.height * 0.8f) , 200, 200));

            if (GUILayout.Button("Generate Project Struct", GUILayout.Height(60) ) )
            {
                GenerateDirectoriesForTheCurrentProject();
            }

            GUILayout.EndArea();
        }


        private void GenerateDirectoriesForTheCurrentProject()
        {
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.iphonePath + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.iphonePath + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.iphonePath + "/" + DIRECTORY_NAME_SMALL);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.androidPath + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.androidPath + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.androidPath + "/" + DIRECTORY_NAME_SMALL);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.windowsPhone8Path + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.windowsPhone8Path + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.windowsPhone8Path + "/" + DIRECTORY_NAME_SMALL);


            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + "/" + DIRECTORY_NAME_SMALL);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + DIRECTORY_NAME_SOUND );
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + DIRECTORY_NAME_DATA);

            string[] foldersName = AFAssetManager.package.Split('.');
            string folder = "";
            int i = 0;

            for (i = 0; i < foldersName.Length; ++i)
            {
                folder += "/" + ( char.ToUpper(foldersName[i][0]) + foldersName[i].Substring(1) );
            }

            UnityEngine.Debug.Log(folder);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + DIRECTORY_NAME_SCRIPTS +  folder);
        }
    }
}
