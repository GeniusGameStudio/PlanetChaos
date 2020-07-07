using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject Start, About, Loading, TeamSeting, GameSeting;
    //点击开始后切换到下一场景
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //设置队伍
    public void Team()
    {
        TeamSeting.SetActive(true);
    }

    //设置游戏
    public void Game()
    {
        GameSeting.SetActive(true);
    }


    //退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }

    //Strat中UI界面的显示
    public void StartEnable()
    {
        Start.SetActive(true);
        Loading.SetActive(false);
    }

    //About界面显示
    public void AboutEnable()
    {
        About.SetActive(true);
    }


    public void Back()
    {
        About.SetActive(false);
        TeamSeting.SetActive(false);
        GameSeting.SetActive(false);
        Start.SetActive(true);
    }
}
