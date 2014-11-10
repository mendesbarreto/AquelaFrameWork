using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Sound;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;
using AquelaFrameWork.Input;

using Signals;

namespace AquelaFrameWork.Core.State
{
    public abstract class AState : AFObject , IState
    {
        
        readonly public static uint STATE_UPDATE = 0x1;
        readonly public static uint STATE_NOT_DESTROYABLE = 0x2;
//         readonly public static uint STATE_SOUND_SUPORT = 0x4;
//         readonly public static uint STATE_ASSET_SUPORT = 0x8;
//         readonly public static uint STATE_INPUT_SUPORT = 0x16;
        readonly public static uint STATE_EVENTS_SUPORT = 0x4;

        readonly public static uint STATE_EVERYTHING = STATE_EVENTS_SUPORT | STATE_NOT_DESTROYABLE | STATE_UPDATE;

        enum EGameState
        {
            STATE_MENU = 0,
            STATE_INTRO,
            STATE_TUTORIAL,
            STATE_RANKING,
            STATE_GAME,
        }

        protected object m_stateManger;
        protected AFSoundManager m_soundManager;
        protected AFAssetManager m_assetManager;
        protected AFInput m_input;
        protected AFEngine m_engine;
        protected List<AFObject> m_objects;

        public Signal<NullSignal> OnStart = new Signal<NullSignal>();
        public Signal<NullSignal> OnDestroy = new Signal<NullSignal>();
        public Signal<NullSignal> OnInitialized = new Signal<NullSignal>();

        public Signal<bool> OnPause = new Signal<bool>();
        public Signal<AFObject> OnObjectAdded = new Signal<AFObject>();
        public Signal<AFObject> OnObjectRemoved = new Signal<AFObject>();

        protected bool m_destroyable;
        protected bool m_update;
        protected bool m_hasEvents;
        protected bool m_initialized;

        private uint ID = 0;

        public AState(uint flags)
        {
            if ((flags & STATE_UPDATE) == STATE_UPDATE)
            {
                m_update = true;
            }
            else
            {
                m_update = false;
            }

            if ((flags & STATE_EVENTS_SUPORT) == STATE_EVENTS_SUPORT)
            {
                m_hasEvents = true;
            }
            else
            {
                m_hasEvents = false;
            }

            if ((flags & STATE_NOT_DESTROYABLE) == STATE_NOT_DESTROYABLE)
            {
                m_destroyable = false;
            }
            else
            {
                m_destroyable = true;
            }
        }


        virtual public void Initialize()
        {
            if (m_hasEvents)
                OnStart.Dispatch();

            if (m_initialized)
            { 
                m_engine = AFEngine.Instance;
                m_soundManager = AFSoundManager.Instance;
                m_assetManager = AFAssetManager.Instance;
                m_input = AFInput.Instance;
                m_objects = new List<AFObject>();

                if (m_hasEvents)
                    OnInitialized.Dispatch();
            }
            else
            {
                Resume();
            }
        }

        public bool IsDestroyable()
        {
            return m_destroyable;
        }

        public void SetDestroyable( bool value )
        {
            m_destroyable = value;
        }


        virtual public void Update( double deltaTime )
        {

        }

        virtual public void Pause()
        {
            m_update = false;

            if (m_hasEvents)
                OnPause.Dispatch(true);
        }

        virtual public void Resume()
        {
            m_update = true;

            if (m_hasEvents)
                OnPause.Dispatch(false);
        }

        virtual public void Destroy()
        {
            if (m_destroyable)
            {
                m_stateManger = null;
                m_soundManager = null;
                m_input = null;
                m_engine = null;
                m_assetManager = null;
                m_objects.Clear();
                m_objects = null;
            }
            else
            {
                Debug.LogWarning("State could not be destroyed, set the destroyable value to true for complete the action");
            }
            
            if (m_hasEvents)
                OnDestroy.Dispatch();
        }

        virtual public AFObject Add(AFObject obj)
        {
            AFObject L_object = GetObjectByName( obj.name );

            if( L_object == null)
            {
                m_objects.Add(obj);
                L_object = obj;
                obj.transform.parent = gameObject.transform;
            }
            else
            {
                UnityEngine.Debug.LogWarning("The object already in the State: " + L_object.name);
            }

            if (m_hasEvents)
                OnObjectAdded.Dispatch(L_object);

            return L_object;
        }

        virtual public Entity AddEntity(Entity entity, object view = null)
        {
            UnityEngine.Debug.LogError("FUNCTION NOT IMPLEMENTED");

            return null;
        }

        virtual public void Remove(AFObject obj)
        {
            if (m_objects.Contains(obj))
                m_objects.Remove(obj);

            if (m_hasEvents)
                OnObjectRemoved.Dispatch(obj);
        }

        virtual public AFObject GetObjectByName(string name)
        {
            foreach( AFObject L_object in m_objects )
            {
                if (L_object.name.Equals(name))
                    return L_object;
            }

            return null;
        }

        virtual public T GetFirstObjectByType<T>() where T : AFObject
        {
            Type L_type = typeof(T);

            for (int i = 0; i < m_objects.Count ; ++i )
            {
                if(L_type.Equals( m_objects[i].GetType() ) )
                    return m_objects[i] as T;
            }

            return null;
        }

        virtual public List<T> GetObjectsByType<T>(Type type) where T : AFObject
        {
            Type L_type = typeof(T);
            List<T> L_list = new List<T>();

            for (int i = 0; i < m_objects.Count; ++i)
            {
                if (L_type.Equals(m_objects[i].GetType()))
                   L_list.Add(m_objects[i] as T);
            }

            return L_list;
        }

    }
}
