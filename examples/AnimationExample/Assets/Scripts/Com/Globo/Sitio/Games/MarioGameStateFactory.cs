using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.State;

public class MarioGameStateFactory : IStateFactory
{

    public IState CreateStateByID(AState.EGameState newstateID)
    {
        switch( newstateID )
        {
            case AState.EGameState.GAME :
                return AFObject.Create<MarioGameState>();
        }


        return null;
    }
}

