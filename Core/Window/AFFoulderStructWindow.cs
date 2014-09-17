using System;
using System.Diagnostics;
using System.IO;

using UnityEditor;
using UnityEngine;

using AFFrameWork.Core.Asset;

namespace AFFrameWork.Core.Window
{
    public class AFFoulderStructWindow : EditorWindow
    {
        static public AFFoulderStructWindow window;

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
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.iphonePath + "/" + AFAssetManager.DIRECTORY_NAME_HIGH);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.iphonePath + "/" + AFAssetManager.DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.iphonePath + "/" + AFAssetManager.DIRECTORY_NAME_LOW);

            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.androidPath + "/" + AFAssetManager.DIRECTORY_NAME_HIGH);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.androidPath + "/" + AFAssetManager.DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.androidPath + "/" + AFAssetManager.DIRECTORY_NAME_LOW);

            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.windowsPhone8Path + "/" + AFAssetManager.DIRECTORY_NAME_HIGH);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.windowsPhone8Path + "/" + AFAssetManager.DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.windowsPhone8Path + "/" + AFAssetManager.DIRECTORY_NAME_LOW);


            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + "/" + AFAssetManager.DIRECTORY_NAME_HIGH);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + "/" + AFAssetManager.DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + "/" + AFAssetManager.DIRECTORY_NAME_LOW);

            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.DIRECTORY_NAME_SOUND);
            Directory.CreateDirectory(AFAssetManager.DIRECTORY_NAME_RESOURCES + "/" + AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.DIRECTORY_NAME_DATA);

            string[] foldersName = AFAssetManager.package.Split('.');
            string folder = "";
            int i = 0;

            for (i = 0; i < foldersName.Length; ++i)
            {
                folder += "/" + ( char.ToUpper(foldersName[i][0]) + foldersName[i].Substring(1) );
            }

            UnityEngine.Debug.Log(folder);
            /*
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + DIRECTORY_NAME_SCRIPTS +  folder);

            GameObject go = new GameObject(typeof(AFEngine).ToString());
            go.AddComponent<AFEngine>();
            go.transform.position = Vector2.zero;
            PrefabUtility.CreatePrefab(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath + go.name + ".prefab", go, ReplacePrefabOptions.ReplaceNameBased);

            UnityEngine.Debug.Log(DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.commumPath);

             * */
        }
    }
}
