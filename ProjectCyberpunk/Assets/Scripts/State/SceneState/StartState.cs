using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller) : base(Controller)
    {
        StateName = "StartState";
    }

    public override void StateUpdate()
    {
        if (Input.anyKey)
        {
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
        }
    }

}
