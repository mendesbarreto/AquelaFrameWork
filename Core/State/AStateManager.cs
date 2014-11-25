using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Factory;

namespace AquelaFrameWork.Core.State
{
    public abstract class AStateManager : AFObject
    {
        ///////////////////////
        // PROPERTIES
        ///////////////////////

        protected AFEngine m_engine;

        [SerializeField]
        protected AState.EGameState currentStateID = AState.EGameState.NULL;
        [SerializeField]
        protected IState m_currentState;
        [SerializeField]
        protected IState m_nextState;

        protected IStateFactory m_factory;
        virtual protected void Awake()
        {
            gameObject.transform.parent = AFEngine.Instance.gameObject.transform;
        }

        virtual public void Initialize( IStateFactory factory )
        {
            //TODO: Verify if factory is null case yes throw some error
            m_factory = factory;
            m_engine = AFEngine.Instance;
        }

        virtual public void AFUpdate(double deltaTime)
        {
            if( m_nextState != null )
                ChangeState();
            UnityEngine.Debug.Log(m_currentState);
            if (m_currentState != null && !currentStateID.Equals(AState.EGameState.NULL) )
                m_currentState.AFUpdate(deltaTime);
        }

        private void ChangeState()
        {
            if (!currentStateID.Equals(AState.EGameState.NULL))
            {
                if (m_currentState.IsDestroyable())
                {
                    m_currentState.Destroy();

                    if (m_currentState is AState)
                        Destroy( ( m_currentState as AState).gameObject );
                }
                else
                {
                    m_currentState.Pause();
                }
            }

            currentStateID = m_nextState.GetStateID();
            m_currentState = m_nextState;

            if (m_currentState is AState)
                (m_currentState as AState).transform.parent = gameObject.transform;

            m_nextState = null;

            m_currentState.Initialize();
        }

        virtual public void GotoState( AState.EGameState newStateID )
        {
            GotoState( m_factory.CreateStateByID(newStateID) );
        }

        virtual public void GotoState( IState newState )
        {
            if (newState == null || newState.GetStateID() == currentStateID)
                return;

            m_nextState = newState;
        }

        virtual public void Pause() { m_currentState.Pause(); }
        virtual public void Resume() { m_currentState.Resume(); }

    }
}
