using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }

    //开始
    public override void StateBegin()
    {
        //数据加载TODO

        //按钮监听
        Button btnStartMenu = UITool.GetUIComponent<Button>("StartMenuButton");

        btnStartMenu.onClick.AddListener(()=> OnStartMenuBtnClick(btnStartMenu));
        
    }

    /// <summary>
    /// 开始主菜单按钮按下
    /// </summary>
    /// <param name="button"></param>
    private void OnStartMenuBtnClick(Button button)
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }
}
