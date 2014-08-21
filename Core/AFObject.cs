using System;
using System.ComponentModel;
using UnityEngine;
using System.Collections;

namespace AFFrameWork.Core
{
    public class AFObject : MonoBehaviour
    {
        
        public static T Create<T>( string name = "") where T : MonoBehaviour
        {
            if( name.Equals("") )
                return new GameObject(typeof(T).ToString()).AddComponent<T>();


            return new GameObject(typeof(T).ToString() + ": " + name).AddComponent<T>();
        }
    }
}
