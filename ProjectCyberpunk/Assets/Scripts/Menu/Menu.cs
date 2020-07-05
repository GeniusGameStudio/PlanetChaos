using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //点击开始后切换到下一场景
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }

    //UI界面的显示
    public void UIEnable()
    {
        GameObject.Find("Canvas_Menu/MainMenu/UI").SetActive(true);
    }

    //About界面显示与关闭
    public void AboutEnable()
    {
        GameObject.Find("Canvas_Menu/MainMenu/About").SetActive(true);
    }
    public void AboutdDisable()
    {
        GameObject.Find("Canvas_Menu/MainMenu/About").SetActive(false);
    }
}
