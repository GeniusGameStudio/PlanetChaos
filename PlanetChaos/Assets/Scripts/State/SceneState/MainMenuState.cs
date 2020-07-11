using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TurnBaseUtil;
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

        Button btnStartMultiplayerGame = UITool.GetUIComponent<Button>("StartMultiplayerGameButton");

        btnStartMultiplayerGame.onClick.AddListener(() => OnStartMultiplayerBtnClick(btnStartMultiplayerGame));

    }

    /// <summary>
    /// 开始战斗场景按钮按下
    /// </summary>
    /// <param name="button"></param>
    private void OnStartGameBtnClick(Button button)
    {
        
        Global.teamA.AddTeamPlayer(new TeamPlayer(UITool.GetUIComponent<Text>("PlayerA1_Name").text, Global.teamA_Color));
        Debug.Log(UITool.GetUIComponent<InputField>("PlayerA1_Input").text);
        Global.teamA.AddTeamPlayer(new TeamPlayer(UITool.GetUIComponent<Text>("PlayerA2_Name").text, Global.teamA_Color));
        Global.teamA.AddTeamPlayer(new TeamPlayer(UITool.GetUIComponent<Text>("PlayerA3_Name").text, Global.teamA_Color));
        Global.teamB.AddTeamPlayer(new TeamPlayer(UITool.GetUIComponent<Text>("PlayerB1_Name").text, Global.teamB_Color));
        Global.teamB.AddTeamPlayer(new TeamPlayer(UITool.GetUIComponent<Text>("PlayerB2_Name").text, Global.teamB_Color));
        Global.teamB.AddTeamPlayer(new TeamPlayer(UITool.GetUIComponent<Text>("PlayerB3_Name").text, Global.teamB_Color));
        m_Controller.SetState(new BattleState(m_Controller), "BattleScene");
    }

    /// <summary>
    /// 开始本地双人游戏按钮按下
    /// </summary>
    /// <param name="button"></param>
    private void OnStartMultiplayerBtnClick(Button button)
    {
        UITool.FindUIGameObject("MainMenuPanel").SetActive(false);
        UITool.FindUIGameObject("TeamSettingsPanel").transform.DOMoveY(0, 1f);
    }
}
