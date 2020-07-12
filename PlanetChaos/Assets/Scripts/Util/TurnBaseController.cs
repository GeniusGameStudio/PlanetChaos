/*
 Author: JackZhang
 Description: 回合制工具类
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace TurnBaseUtil
{
    public class TurnBaseController
    {
        //当前回合数
        public int currentTurnIndex = 1;

        //队伍列表
        private List<Team> teams = new List<Team>();

        //当前轮到的队伍序列号
        private int currentTurnTeamIndex = 0;

        //回合参数
        private TurnProperties turnProperties = new TurnProperties();
        public TurnProperties TurnProperties { get { return turnProperties; } set { turnProperties = value; } }

        public TurnBaseController()
        {

        }

        public UnityEvent OnTurnStart = new UnityEvent();
        public UnityEvent OnTurnEnd = new UnityEvent();

        #region 队伍操作

        /// <summary>
        /// 添加队伍
        /// </summary>
        /// <param name="team"></param>
        public void AddTeam(Team team)
        {
            teams.Add(team);
            team.Index = teams.Count - 1;
        }

        /// <summary>
        /// 移除队伍
        /// </summary>
        /// <param name="team"></param>
        public void RemoveTeam(Team team)
        {
            try
            {
                teams.Remove(team);
            }
            catch (Exception e)
            {
                GameManager.Instance.LogError(e.Message);
            }
        }

        /// <summary>
        /// 获取队伍（根据index)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Team GetTeam(int index)
        {
            try
            {
                return teams[index];
            }
            catch (Exception e)
            {
                GameManager.Instance.LogError(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取当前回合轮到的队伍
        /// </summary>
        /// <returns></returns>
        public Team GetCurrentTurnTeam()
        {
            return teams[GetCurrentTurnTeamIndex()];
        }

        /// <summary>
        /// 获取队伍总数
        /// </summary>
        /// <returns></returns>
        public int GetTeamCount()
        {
            return teams.Count;
        }

        /// <summary>
        /// 获取所有队伍玩家数
        /// </summary>
        /// <returns></returns>
        public int GetAllTeamPlayerCount()
        {
            int tmp = 0;
            foreach(var team in teams)
            {
                tmp += team.teamPlayers.Count;
            }
            return tmp;
        }

        #endregion

        #region 回合操作

        /// <summary>
        /// 开始回合
        /// </summary>
        public void StartTurn()
        {

            //GameManager.Instance.Log("当前回合队伍index" + currentTurnTeamIndex);
            Team currentTeam = teams[currentTurnTeamIndex];
            currentTeam.IsSelfTurn = true;
            if (currentTeam.GetCurrentTurnPlayer() != null)
            {
                currentTeam.GetCurrentTurnPlayer().PlayerController.enabled = true;
            }
            //初始化回合参数（风力等。。)
            turnProperties.WindForce = new Vector2(Random.Range(-5f, 5f) * 50, 0);
            OnTurnStart.Invoke();

        }

        /// <summary>
        /// 结束回合
        /// </summary>
        public void EndTurn()
        {
            Team currentTeam = teams[currentTurnTeamIndex];
            currentTeam.IsSelfTurn = false;
            if(currentTeam.GetCurrentTurnPlayer() != null)
            {
                currentTeam.GetCurrentTurnPlayer().PlayerController.enabled = false;
            }
            currentTeam.NextTurn();
            NextTurn();

            OnTurnEnd.Invoke();
        }

        /// <summary>
        /// 获取当前轮到的队伍的Index
        /// </summary>
        /// <returns></returns>
        public int GetCurrentTurnTeamIndex()
        {
            return currentTurnTeamIndex;
        }

        /// <summary>
        /// 获取下一轮队伍的Index
        /// </summary>
        /// <returns></returns>
        public int NextTurnTeamIndex()
        {
            if (currentTurnTeamIndex + 1 >= teams.Count)
            {
                return 0;
            }
            return currentTurnTeamIndex + 1;
        }

        /// <summary>
        /// 进入下一轮
        /// </summary>
        public void NextTurn()
        {
            currentTurnTeamIndex++;
            ClampTurnIndex();
        }

        /// <summary>
        /// 确保Index不越界，按周期进行
        /// </summary>
        public void ClampTurnIndex()
        {
            if (currentTurnTeamIndex >= teams.Count)
            {
                currentTurnTeamIndex = 0;
            }
        }

        #endregion
    }
}

