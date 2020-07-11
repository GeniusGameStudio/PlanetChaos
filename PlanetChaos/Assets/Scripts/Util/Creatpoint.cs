using System.Collections;
using System.Collections.Generic;
using TurnBaseUtil;
using UnityEngine;

public class Creatpoint : MonoBehaviour
{
    public List<GameObject> gameObjects=new List<GameObject>();//存放出生点的集合
    public GameObject playerTeam1;//队伍一角色预制体
    public GameObject playerTeam2;//队伍二角色预制体
    public int teamNumber=3;//一个队伍的人数
    int[] m = new int[6] { -1, -1,-1,-1,-1,-1};
    void Start()
    {
        for(int i=0; i < 6; i++)//获取六个不重复的生成点编码
        {
            while (true)
            {
                int o = 0;
                int n = Random.Range(0, gameObjects.Count);
                for(int j = 0; j <= i; j++)
                {
                    if (m[j] == n)
                    {
                        o++;
                    }
                }
                if (o == 0)
                {
                    m[i] = n;
                    break;
                }
            }
        }
        
        for(int i = 0; i < teamNumber; i++)//根据随机数随机出生点生成角色
        {
           
            GameObject player1 = Instantiate(playerTeam1, gameObjects[m[i*2]].transform.position, gameObjects[m[i*2]].transform.rotation);
            player1.GetComponent<TeamPlayer>().Copy(Global.teamA.teamPlayers[i]);
            GameManager.Instance.teamA_Players.Add(player1);
            GameObject player2 = Instantiate(playerTeam2, gameObjects[m[i * 2+1]].transform.position, gameObjects[m[i * 2+1]].transform.rotation);
            player2.GetComponent<TeamPlayer>().Copy(Global.teamB.teamPlayers[i]);
            GameManager.Instance.teamB_Players.Add(player2);
        }
    }

 
}
