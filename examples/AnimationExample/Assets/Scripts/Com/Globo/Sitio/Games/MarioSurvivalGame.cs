using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

public class MarioSurvivalGame :  AFEngine 
{


    // Initialize your game
    public override void Initialize()
    {
        // You need create a StateManger
        m_stateManager = AFObject.Create<AFStateManager>();

        // You need a factory of states to initialize your engine, here is where you will declarate all your states.
        m_stateManager.Initialize( new MarioGameStateFactory() );

        //Sending the state to game state
        m_stateManager.GotoState(AState.EGameState.GAME);

        //Call the Init of AFEngine
        base.Initialize();
    }	
}
