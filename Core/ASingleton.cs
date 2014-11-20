using System.Collections;

using UnityEngine;

namespace AquelaFrameWork.Core
{
    public abstract class ASingleton<T> : AFObject where T : MonoBehaviour
    {
        protected static T m_instance;

        protected ASingleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = AFObject.Create<T>();
                    GameObject.DontDestroyOnLoad(m_instance);
                }
                return m_instance;
            }
        }

    }
}
