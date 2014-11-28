﻿using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using AquelaFrameWork.Sound;

namespace AquelaFrameWork.Core.Asset
{

    public class AFAssetManager : ASingleton<AFAssetManager>
    {
        public static string iphonePath = "IOS/";
        public static string androidPath = "Android/";
        public static string windowsPhone8Path = "WP8/";
        public static string commumPath = "Common/";
        public static string package = "com.globo.sitio.games";

        public static readonly string DIRECTORY_NAME_ASSETS = "Assets";
        public static readonly string DIRECTORY_NAME_HIGH = "High";
        public static readonly string DIRECTORY_NAME_XHIGH = "ExtraHigh";
        public static readonly string DIRECTORY_NAME_LOW = "Low";
        public static readonly string DIRECTORY_NAME_MEDIUM = "Medium";
        public static readonly string DIRECTORY_NAME_SOUND = "Sounds";
        public static readonly string DIRECTORY_NAME_DATA = "Data";
        public static readonly string DIRECTORY_NAME_SCRIPTS = "Scripts";
        public static readonly string DIRECTORY_NAME_RESOURCES= "Resources";

        protected Dictionary<string, Texture> m_textures = new Dictionary<string,Texture>();
        protected Dictionary<string, AFTextureAtlas> m_texturesAtlas = new Dictionary<string, AFTextureAtlas>();
        protected Dictionary<string , AFSound> m_sounds = new Dictionary<string,AFSound>();
        protected Dictionary<string , GameObject> m_prefabs = new Dictionary<string,GameObject>();
        protected Dictionary<string , object> m_custom = new Dictionary<string,object>();

        protected Dictionary<string, AFPool> m_pool = new Dictionary<string, AFPool>();
        void Awake()
        {
            gameObject.transform.parent = AFEngine.Instance.gameObject.transform;
        }

        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return Load<T>(path, path);
        }

        public T Load<T>(string name, string path) where T : UnityEngine.Object
        {
            T res = null;

            try
            {
                res = GetAsset<T>(name);

                if (res == null)
                {

                    if (typeof(T) == typeof(AFTextureAtlas))
                    {
                        res = new AFTextureAtlas("sprites", path, AFTextureAtlas.EFileType.kTextTypes_Csv) as T;
                    }
                    else
                    {
                        res = Resources.Load<T>(path);
                    }

                    UnityEngine.Debug.Log("I'll store an object of: " + typeof(T).ToString());

                    Add(name, res);
                }
            }
            catch( NullReferenceException nullEx )
            {
                UnityEngine.Debug.LogError("The asset was not found: " + nullEx.Message);
            }
            catch( Exception ex )
            {
                //TODO: Discover what happens when unity throw this error
                UnityEngine.Debug.LogError("The asset was not found: " + ex.Message);
            }

            return res;
        }


        public AFPool CreatePool<T>( string name , string assetName , uint qtd , uint maxPoolObjects = 0) where T : UnityEngine.Object
        {
            AFPool pool = null;

            if( qtd > 1 )
            {

                T obj = GetAsset<T>(assetName);

                if ( obj != null )
                {

                    pool = AFObject.Create<AFPool>(name);
                    pool.Init(name, maxPoolObjects );

                    pool.transform.parent = this.gameObject.transform;

                    if( obj.GetType().IsSubclassOf( typeof(UnityEngine.Object) ) )
                    {
                        UnityEngine.Debug.Log("Creating a object from a instance");
                        pool.Create((UnityEngine.Object)obj, qtd);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Creating a object from a Type");
                        pool.Create(obj.GetType(), (int)qtd );
                    }

                    m_pool.Add(name, pool);
                }
                else
                {
                    UnityEngine.Debug.LogError(" The asset not exists " + assetName);
                }
            }
            else
            {
                UnityEngine.Debug.LogError(" The pool should be more than one");
            }

            return pool;
        }

        public object Add(string name, object obj)
        {
            if( obj != null )
            {
                m_custom.Add(name , obj);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Object could not be null");
            }

            return obj;
        }

        public AFSound Add(string name, AFSound sound)
        {
            if ( sound != null)
            {
                m_sounds.Add(name, sound);
            }
            else
            {
                UnityEngine.Debug.LogWarning("AFSound could not be null");
            }

            return sound;
        }

        public GameObject Add(string name, GameObject gameObject)
        {
            if ( gameObject != null)
            {
                m_prefabs.Add(name, gameObject);
            }
            else
            {
                UnityEngine.Debug.LogWarning("GameObject could not be null");
            }

            return gameObject;
        }

        public AFTextureAtlas Add(string name, AFTextureAtlas textureAtlas)
        {
            if (textureAtlas != null)
            {
                m_texturesAtlas.Add(name, textureAtlas);
            }
            else
            {
                UnityEngine.Debug.LogWarning("AFTextureAtlas could not be null");
            }

            return textureAtlas;
        }

        public Texture Add(string name, Texture texture)
        {
            if (texture != null)
            {
                m_textures.Add(name, texture);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Texture could not be null");
            }

            return texture;
        }

        public Texture GetTexture( string name )
        {
            if( m_textures.ContainsKey( name ) )
            {
                return m_textures[name];
            }
            
            UnityEngine.Debug.LogWarning("Texture " + name + " was not found!");
            return null;
        }

        public GameObject GetPrefabs( string name )
        {
            if( m_prefabs.ContainsKey( name ) )
            {
                return m_prefabs[name];
            }
            
            UnityEngine.Debug.LogWarning( "Prefeb: " + name + " was not found!" );


            return null;
        }

        public AFSound GetAFSound( string name )
        {
            if( m_sounds.ContainsKey( name ) )
            {
                return m_sounds[name];
            }
            
            UnityEngine.Debug.LogWarning(" AFSound: " + name + " was not found!");

            return null;
        }

        public void Delete( string name )
        {
            if( m_textures.ContainsKey(name ) )
            {
                m_textures.Remove(name);
            }
            else if ( m_sounds.ContainsKey(name))
            {
                m_sounds[name].Destroy();
                m_sounds.Remove(name);
            }
            else if( m_prefabs.ContainsKey(name ) )
            {
                GameObject.Destroy(m_prefabs[name]);
                m_prefabs.Remove(name);
            }
            else if( m_custom.ContainsKey(name ) )
            {
                m_custom.Remove(name);
            }
            else
            {
                UnityEngine.Debug.LogWarning("The asset: " + name + " was not found");
            }
        }

        public bool HasAsset( string name )
        {
            if ( m_textures.ContainsKey(name) || 
                m_sounds.ContainsKey(name) ||
                m_prefabs.ContainsKey(name) ||
                m_custom.ContainsKey(name))
            {
                return true;
            }
            
            return false;
        }

        private T GetAsset<T>(string name) where T : UnityEngine.Object
        {
            T obj = null;

            if (m_textures.ContainsKey(name))
            {
                obj = m_textures[name] as T;
            }
            if(m_sounds.ContainsKey(name) )
            {
                obj = m_sounds[name] as T;
            }
            if(m_prefabs.ContainsKey(name))
            {
                obj = m_prefabs[name] as T;
            }
            if (m_custom.ContainsKey(name))
            {
                obj = m_custom[name] as T;
            }

            return obj;
        }


        public void DisposeAll()
        {
            m_textures = new Dictionary<string,Texture>();
            m_sounds = new Dictionary<string,AFSound>();
            m_prefabs = new Dictionary<string,GameObject>();
            m_custom = new Dictionary<string,object>();
        }

        public static string GetPathTargetPlatform()
        {
            #if UNITY_IPHONE
                return iphonePath;
            #elif UNITY_ANDROID
                return androidPath;
            #elif UNITY_WP8
                return windowsPhone8Path;
            #else
            return GetCommumPath();
            #endif
        }

        public static string GetPathTargetPlatformWithResolution()
        {
            return ( GetPathTargetPlatform() + GetResolutionFolder() );
        }


        public static string GetResolutionFolder()
        {
            UnityEngine.Debug.Log("DPI DA TELA: " + Screen.dpi);

            if (Screen.dpi > 290 )
            {
                return DIRECTORY_NAME_XHIGH;
            }
            else if (Screen.dpi > 200 && Screen.dpi <= 290 )
            {
                return DIRECTORY_NAME_HIGH;
            }
            else if ( Screen.dpi >= 150  && Screen.dpi <= 200 )
            {
                return DIRECTORY_NAME_MEDIUM;
            }
            else if( Screen.dpi < 150 )
            {
                return DIRECTORY_NAME_LOW;
            }

            return DIRECTORY_NAME_LOW;
        }

        public static string GetCommumPath()
        {
            return commumPath;
        }

    }
}