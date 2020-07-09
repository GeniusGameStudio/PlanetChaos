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

    void Start()
    {
        
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
