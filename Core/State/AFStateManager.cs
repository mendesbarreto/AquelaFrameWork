using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core.Factory;

namespace AquelaFrameWork.Core.State
{
    public class AFStateManager : AStateManager
    {
        public AFStateManager( IStateFactory factory ) : 
            base(factory)
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
