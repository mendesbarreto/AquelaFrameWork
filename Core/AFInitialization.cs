using System;
using UnityEditor;
using UnityEngine;

using AFFrameWork.Core.Asset;
using AFFrameWork.Core.Window;

namespace AFFrameWork.Core
{
    [InitializeOnLoad]
    public class AFInitialization
    {
        static AFInitialization()
        {
            UnityEngine.Debug.Log("I am awaking");
            EditorApplication.playmodeStateChanged += OnPlayModeStateChangedHandler;
        }

        private static void OnPlayModeStateChangedHandler()
        {
            if (EditorApplication.isPlaying && !EditorApplication.isPaused)
            {
                UnityEngine.Debug.Log("Editor is playing");
                AddGameEngineObject();
                
            }
            else if (EditorApplication.isPaused)
            {
                UnityEngine.Debug.Log("Editor is pause");
            }
            else
            {
                UnityEngine.Debug.Log("Editor stops");
            }
        }


        private static void AddGameEngineObject()
        {
            GameObject go;
            
            go = GameObject.Find( typeof(AFEngine).ToString() );

            //UnityEngine.Debug.Log(go);

            if ( go == null )
            {
                go = new GameObject(typeof(AFEngine).ToString());
                go.AddComponent<AFEngine>();
                go.transform.position = Vector2.zero;
                PrefabUtility.CreatePrefab(AFAssetManager.DIRECTORY_NAME_ASSETS + "/" + AFAssetManager.DIRECTORY_NAME_RESOURCES +"/" + AFAssetManager.commumPath + go.name + ".prefab", go, ReplacePrefabOptions.ReplaceNameBased);
            }
            
        }

    }
}
