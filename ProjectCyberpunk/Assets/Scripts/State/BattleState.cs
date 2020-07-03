using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : ISceneState
{
    public BattleState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "BattleState";
    }


}
