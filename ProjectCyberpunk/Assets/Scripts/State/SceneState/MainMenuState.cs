﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainMenuState";
    }

    //开始
    public override void StateBegin()
    {
        //数据加载TODO

        //按钮监听
        Button btnStartGame = UITool.GetUIComponent<Button>("StartGameButton");

        btnStartGame.onClick.AddListener(() => OnStartGameBtnClick(btnStartGame));

    }

    /// <summary>
    /// 开始战斗场景按钮按下
    /// </summary>
    /// <param name="button"></param>
    private void OnStartGameBtnClick(Button button)
    {
        m_Controller.SetState(new BattleState(m_Controller), "BattleScene");
    }
}