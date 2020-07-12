using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Global;

public class ItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    
    
    public Text countText;

    [TextArea]
    public string infoTextString;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerController currentPlayer = GameManager.Instance.TurnBaseController.GetCurrentTurnTeam().GetCurrentTurnPlayer().PlayerController;
        currentPlayer.useWeapon[(int)Weapon.BAZOOKA] = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.SetInfoTipText(infoTextString);
        UIManager.Instance.SetInfoTipActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.SetInfoTipText("");
        UIManager.Instance.SetInfoTipActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
