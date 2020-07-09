using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text playerName_Text;
    public Text playerHP_Text;
    public Transform playerArrow;

    /// <summary>
    /// 更新玩家名称
    /// </summary>
    /// <param name="name"></param>
    public void UpdateName(string name)
    {
        playerName_Text.text = name;
    }

    /// <summary>
    /// 更新玩家血量数字
    /// </summary>
    /// <param name="hp"></param>
    public void UpdatePlayerHP(int hp)
    {
        playerHP_Text.text = hp.ToString();
    }

    /// <summary>
    /// 设置玩家头顶箭头的可见性
    /// </summary>
    /// <param name="isActive"></param>
    public void SetArrowActive(bool isActive)
    {
        playerArrow.gameObject.SetActive(isActive);
    }
}
