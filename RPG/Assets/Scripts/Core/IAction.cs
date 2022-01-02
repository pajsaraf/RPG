using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public interface IAction   //everything in interface is auto public - we will use this to call all actions by player from other class's
    {
        void Cancel();


    }

}
