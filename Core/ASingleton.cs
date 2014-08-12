using UnityEngine;
using System.Collections;

namespace AFFrameWork.Core
{
    public abstract class ASingleton<T> : AFObject where T : MonoBehaviour
    {
        protected static T instance;

        protected ASingleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = AFObject.Create<T>();
                    GameObject.DontDestroyOnLoad( instance );
                }
                return instance;
            }
        }

    }
}
