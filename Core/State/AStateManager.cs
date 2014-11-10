using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;

namespace AquelaFrameWork.Core.State
{
    public class AStateManager
    {
        ///////////////////////
        // PROPERTIES
        ///////////////////////

        protected AFEngine m_engine;

        protected IState m_currentState;
        protected IState m_nextState;


        ///////////////////////
        // METHODS
        ///////////////////////

        virtual public void Update( double deltaTime )
        {
            if( m_nextState != null )
                ChangeState();
            
            if(m_currentState != null)
                m_currentState.Update(deltaTime);
        }

        private void ChangeState()
        {
            if( m_currentState.IsDestroyable() )
            {
                m_currentState.Destroy();
            }
            else
            {
                m_currentState.Pause();
            }

            m_currentState = m_nextState;
            m_currentState.Initialize();
        }

        virtual public void GotoState( IState newState )
        {
            if (newState == null || newState.GetID() == m_currentState.GetID())
                return;

            m_nextState = newState;
        }


    }
}
