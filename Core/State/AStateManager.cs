using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        protected AState.EGameState currentStateID = AState.EGameState.NULL_ID;
        protected IState m_currentState;
        protected IState m_nextState;

        protected IStateFactory m_factory;

        virtual protected void Awake()
        {

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

            if (m_currentState != null && !currentStateID.Equals(AState.EGameState.NULL_ID) )
                m_currentState.AFUpdate(deltaTime);
        }

        private void ChangeState()
        {
            if (!currentStateID.Equals(AState.EGameState.NULL_ID))
            {
                if (m_currentState.IsDestroyable())
                {
                    m_currentState.Destroy();
                }
                else
                {
                    m_currentState.Pause();
                }
            }

            currentStateID = m_nextState.GetStateID();
            m_currentState = m_nextState;
            m_currentState.Initialize();
            m_nextState = null;
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
