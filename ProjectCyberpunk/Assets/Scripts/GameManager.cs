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

    //A队 队员
    public TeamPlayer[] teamPlayersA;

    //B队 队员
    public TeamPlayer[] teamPlayersB;

    public TurnBaseController TurnBaseController { get; set; }

    private Team teamA;
    private Team teamB;

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

    void InitGame()
    {
        TurnBaseController = new TurnBaseController();
        Log("GameInit");
        vCam.Follow = null;

        teamA = new Team("A");
        foreach (var teamPlayer in teamPlayersA)
        {
            teamA.AddTeamPlayer(teamPlayer);
            teamPlayer.PlayerController.enabled = false;
            teamPlayer.belongsTo = teamA;
        }

        teamB = new Team("B");
        foreach (var teamPlayer in teamPlayersB)
        {
            teamB.AddTeamPlayer(teamPlayer);
            teamPlayer.PlayerController.enabled = false;
            teamPlayer.belongsTo = teamB;
        }

        TurnBaseController.AddTeam(teamA);
        TurnBaseController.AddTeam(teamB);

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
        StartCoroutine(DelayFuc(() => { InitGame(); }, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
