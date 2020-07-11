using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TurnBaseUtil;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager的一个单例
    public static GameManager Instance;
    public CinemachineVirtualCamera vCam;

    public TurnBaseController TurnBaseController { get; set; }
    [HideInInspector]
    public List<GameObject> teamA_Players;
    [HideInInspector]
    public List<GameObject> teamB_Players;

    public IEnumerator DelayFuc(Action action, float delaySeconds)
    {
        Log("StartDelay");
        yield return new WaitForSeconds(delaySeconds);
        action();
        Log("EndDelay");
    }

    public void Log(string msg)
    {
        Debug.Log(msg);
    }

    public void LogError(string errormsg)
    {
        Debug.LogError(errormsg);
    } 

    public void InitGame()
    {
        
        Log("GameInit");
        vCam.Follow = null;

        Global.teamA.teamPlayers.Clear();
        Global.teamB.teamPlayers.Clear();

        foreach (var teamA_Player in teamA_Players)
        {
            TeamPlayer teamPlayer = teamA_Player.GetComponent<TeamPlayer>();
            teamPlayer.InitUI();
            Global.teamA.AddTeamPlayer(teamPlayer);
            teamPlayer.PlayerController.enabled = false;
        }

        Global.teamA.InitHP();

        foreach (var teamB_Player in teamB_Players)
        {
            TeamPlayer teamPlayer = teamB_Player.GetComponent<TeamPlayer>();
            teamPlayer.InitUI();
            Global.teamB.AddTeamPlayer(teamPlayer);
            teamPlayer.PlayerController.enabled = false;
        }

        Global.teamB.InitHP();

        TurnBaseController.AddTeam(Global.teamA);
        TurnBaseController.AddTeam(Global.teamB);

        TurnBaseController.StartTurn();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        TurnBaseController = new TurnBaseController();
        StartCoroutine(DelayFuc(() => { SendMessage("GameInited"); InitGame(); }, 0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
