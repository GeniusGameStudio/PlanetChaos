using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleState : ISceneState
{
    private bool isPaused;

    private RectTransform canvasRectTransform;

    private RectTransform menuPanelRectTransform;

    public BattleState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "BattleState";
    }

    public override void StateBegin()
    {
        //BGM切换
        GameObject gameloop = UnityTool.FindGameObject("GameLoop");
        GameLoop gameLoopScript = gameloop.GetComponent<GameLoop>();
        AudioSource audio = gameloop.GetComponent<AudioSource>();
        audio.clip = gameLoopScript.battleGame;
        audio.Play();

        GameObject canvas = UITool.FindUIGameObject("Canvas");
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        GameObject menuPanel = UITool.FindUIGameObject("MenuPanel");
        menuPanelRectTransform = menuPanel.GetComponent<RectTransform>();

        //按键绑定
        Button btnResumeGame = UITool.GetButton("ResumeGameButton");
        btnResumeGame.onClick.AddListener(() => ResumeGame(btnResumeGame));

        Button btnBackToMainMenu = UITool.GetButton("BackToMainMenuButton");
        btnBackToMainMenu.onClick.AddListener(() =>BackToMainMenu(btnBackToMainMenu));

        Button btnExitGame = UITool.GetButton("ExitGameButton");
        btnExitGame.onClick.AddListener(() => ExitGame(btnExitGame));

        Button btnEndTurn = UITool.GetButton("EndTurnButton");
        btnEndTurn.onClick.AddListener(() => EndTurn(btnEndTurn));

        Button btnBackToMainMenu2 = UITool.GetButton("BackToMainMenuButton2");
        btnBackToMainMenu2.onClick.AddListener(() => BackToMainMenu2(btnBackToMainMenu2));
    }

    public override void StateUpdate()
    {
        //按键监听
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                //暂停游戏
                Time.timeScale = 0;
                menuPanelRectTransform.DOLocalMoveX(-canvasRectTransform.rect.width / 2, 0.5f).SetUpdate(true);
                isPaused = true;
                if(!UIManager.Instance.isEnd)
                    UIManager.Instance.SetEndTurnButtonActive(false);
            }
            else
            {
                //恢复游戏
                Time.timeScale = 1;
                menuPanelRectTransform.DOLocalMoveX(-canvasRectTransform.rect.width / 2 - 300f, 0.5f).SetUpdate(true);
                isPaused = false;
                if (!UIManager.Instance.isEnd)
                    UIManager.Instance.SetEndTurnButtonActive(true);
            }
            
        }
    }

    /// <summary>
    /// 直接结束本回合
    /// </summary>
    /// <param name="button"></param>
    private void EndTurn(Button button)
    {
        GameManager.Instance.TurnBaseController.EndTurn();
        GameManager.Instance.TurnBaseController.StartTurn();
    }

    private void ResumeGame(Button button)
    {
        //恢复游戏
        Time.timeScale = 1;
        menuPanelRectTransform.DOLocalMoveX(-canvasRectTransform.rect.width / 2 - 300f, 0.5f).SetUpdate(true);
        isPaused = false;
    }

    private void BackToMainMenu(Button button)
    {
        //恢复游戏
        Time.timeScale = 1;
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }

    private void ExitGame(Button button)
    {
        Application.Quit();
    }

    private void BackToMainMenu2(Button button)
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }

}
