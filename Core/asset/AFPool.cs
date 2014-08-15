using System;
using System.Collections.Generic;


using UnityEngine;

namespace AFFrameWork.Core.asset
{
    public class AFPool<T> where T : object
    {
        private string m_name = "";
        private List<T> m_pooledObjs = new List<T>();
        
        private uint m_qtdObjectsPooled = 0;
        private uint m_maxObjects = 0;

        public AFPool( string name , uint maxObjects = 0)
        {
            m_name = name;
            m_maxObjects = maxObjects;
        }

        public void Add( T obj )
        {
            if (m_maxObjects > 0 && m_qtdObjectsPooled <= m_maxObjects)
            { 
                m_pooledObjs.Add(obj);
                m_qtdObjectsPooled++;
            }
            else
            {
                UnityEngine.Debug.LogWarning("The object could not be added, because the pool reached your max limit of: " + m_maxObjects);
            }
        }

        public void Destroy()
        {
            m_qtdObjectsPooled = 0;
            m_pooledObjs.Clear();
            m_pooledObjs = null;
        }


    }
}
