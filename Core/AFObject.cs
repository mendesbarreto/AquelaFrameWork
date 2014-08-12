using System;
using System.ComponentModel;
using UnityEngine;
using System.Collections;

namespace AFFrameWork.Core
{
    public class AFObject : MonoBehaviour
    {
        
        public static T Create<T>() where T : MonoBehaviour
        {
            return new GameObject(typeof(T).ToString()).AddComponent<T>();
        }
    }
}
