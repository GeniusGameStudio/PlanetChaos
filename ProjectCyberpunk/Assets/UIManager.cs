using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    public UnityAction turnStartAction;
    public UnityAction turnEndAction;

    public Text windForceValueText;

    public RectTransform teamA_HP_Mask;
    private float maskA_Width;

    public RectTransform teamB_HP_Mask;
    private float maskB_Width;

    void Start()
    {
        maskA_Width = teamA_HP_Mask.sizeDelta.x;
        maskB_Width = teamB_HP_Mask.sizeDelta.x;
    }

    public void UpdateTeamA_HP_UI(float percentage)
    {
        teamA_HP_Mask.sizeDelta = new Vector2(maskA_Width * percentage, teamA_HP_Mask.sizeDelta.y);
    }

    public void UpdateTeamB_HP_UI(float percentage)
    {
        teamB_HP_Mask.sizeDelta = new Vector2(maskB_Width * percentage, teamB_HP_Mask.sizeDelta.y);
    }

    public void GameInited()
    {
        Debug.Log("UI Init");
        turnStartAction = new UnityAction(OnTurnStartAction);
        turnEndAction = new UnityAction(OnTurnEndAction);
        GameManager.Instance.TurnBaseController.OnTurnStart.AddListener(turnStartAction);
        GameManager.Instance.TurnBaseController.OnTurnEnd.AddListener(turnEndAction);
    }

    void OnTurnStartAction()
    {
        Debug.Log("TurnStart");
        //回合开始，设置风力值
        windForceValueText.text = GameManager.Instance.TurnBaseController.TurnProperties.WindForce.x.ToString();
    }

    void OnTurnEndAction()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
