using System;
using System.Collections.Generic
using UnityEngine;

using AFFrameWork.Sound;

namespace AFFrameWork.Core
{

    public class AFAssetManager : ASingleton<AFAssetManager>
    {
        protected Dictionary<string, Texture> m_textures = new Dictionary<string,Texture>();

        protected Dictionary<string , AFSound> m_sounds = new Dictionary<string,AFSound>();

        protected Dictionary<string , GameObject> m_prefabs = new Dictionary<string,GameObject>();

        protected Dictionary<string , object> m_custom = new Dictionary<string,object>();

        
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
            
            UnityEngine.Debug.LogWarning("Prefeb: " + name + " was not found!");


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

        public AFSound GetAFSound( string name )
        {
            if( m_sounds.ContainsKey( name ) )
            {
                return m_sounds[name];
            }
            
            UnityEngine.Debug.LogWarning(" AFSound: " + name + " was not found!");


            return null;
        }



    }
}
