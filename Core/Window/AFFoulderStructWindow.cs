using System;
using System.Diagnostics;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace AFFrameWork.Core.Window
{

    
    public class AFFoulderStructWindow : EditorWindow
    {
        static public AFFoulderStructWindow window;

        private string m_iphonePath = "Resources/IOS/";
        private string m_androidPath = "Resources/Android/";
        private string m_windowsPhone8Path = "Resources/WP8/";
        private string m_commumPath = "Resources/Commum/";

        private string m_package = "com.globo.sitio.games";

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
            m_package = EditorGUILayout.TextField(m_package);

            GUILayout.Label("Iphone Path ");
            m_iphonePath = EditorGUILayout.TextField(m_iphonePath);

            GUILayout.Label("Android Path");
            m_androidPath = EditorGUILayout.TextField(m_androidPath);

            GUILayout.Label("Windows Phone 8 ");
            m_windowsPhone8Path = EditorGUILayout.TextField(m_windowsPhone8Path);

            GUILayout.BeginArea( new Rect( (window.position.width * 0.5f) - 100 , (window.position.height * 0.8f) , 200, 200));

            if (GUILayout.Button("Generate Project Struct", GUILayout.Height(60) ) )
            {
                GenerateDirectoriesForTheCurrentProject();
            }

            GUILayout.EndArea();
        }


        private void GenerateDirectoriesForTheCurrentProject()
        {
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/"  + m_iphonePath + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_iphonePath + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_iphonePath + "/" + DIRECTORY_NAME_SMALL);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_androidPath + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_androidPath + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_androidPath + "/" + DIRECTORY_NAME_SMALL);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_windowsPhone8Path + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_windowsPhone8Path + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_windowsPhone8Path + "/" + DIRECTORY_NAME_SMALL);


            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_commumPath + "/" + DIRECTORY_NAME_LARGE);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_commumPath + "/" + DIRECTORY_NAME_MEDIUM);
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + m_commumPath + "/" + DIRECTORY_NAME_SMALL);

            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + DIRECTORY_NAME_SOUND );
            Directory.CreateDirectory(DIRECTORY_NAME_ASSETS + "/" + DIRECTORY_NAME_DATA);

            string[] foldersName = m_package.Split('.');
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
