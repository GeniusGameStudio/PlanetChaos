/*
 Author: JackZhang
 Description: 队伍类
 */


using System;
using System.Collections;
using System.Collections.Generic;

namespace TurnBaseUtil
{
    public class Team
    {
        //队员列表
        private List<TeamPlayer> teamPlayers = new List<TeamPlayer>();

        //本回合，轮到的队员序号
        private int currentTurnPlayerIndex = 0;

        //队伍名称
        private string name;
        public string Name { get { return name; } set { name = value; } }

        //是否是自己的回合
        private bool isSelfTurn = false;
        public bool IsSelfTurn { get { return isSelfTurn; } set { isSelfTurn = value; } }

        //队伍序号
        private int index;
        public int Index { get { return index; } set { index = value; } }

        //队伍总血量
        private int totalHP;
        public int TotalHP { get { return totalHP; } set { totalHP = value; } }

        private int maxHP;

        public Team(string name)
        {
            this.name = name;
        }

        public void InitHP()
        {
            totalHP = 100 * teamPlayers.Count;
            maxHP = totalHP;
        }

        public void UpdateHP()
        {
            int tmpHP = 0;
            foreach(var teamPlayer in teamPlayers)
            {
                tmpHP += teamPlayer.hp;
            }
            totalHP = tmpHP;

            bool isTeamA = (Name == "A");

            if(isTeamA)
                UIManager.Instance.UpdateTeamA_HP_UI(totalHP * 1.0f / maxHP);
            else
                UIManager.Instance.UpdateTeamB_HP_UI(totalHP * 1.0f / maxHP);
        }

        /// <summary>
        /// 添加队员
        /// </summary>
        /// <param name="teamPlayer"></param>
        public void AddTeamPlayer(TeamPlayer teamPlayer)
        {
            teamPlayers.Add(teamPlayer);
            teamPlayer.Index = teamPlayers.Count - 1;
        }

        /// <summary>
        /// 移除队员
        /// </summary>
        /// <param name="teamPlayer"></param>
        public void RemoveTeamPlayer(TeamPlayer teamPlayer)
        {
            try
            {
                teamPlayers.Remove(teamPlayer);
            }
            catch(Exception e)
            {
                GameManager.Instance.LogError(e.Message);
            }
        }

        /// <summary>
        /// 获得当前回合，当前队员
        /// </summary>
        /// <returns></returns>
        public TeamPlayer GetCurrentTurnPlayer()
        {
            if (currentTurnPlayerIndex < teamPlayers.Count)
                return teamPlayers[GetCurrentTurnPlayerIndex()];
            else
                return null;
        }

        /// <summary>
        /// 获取当前回合，当前队员的Index
        /// </summary>
        /// <returns></returns>
        public int GetCurrentTurnPlayerIndex()
        {
            GameManager.Instance.Log(currentTurnPlayerIndex.ToString());
            return currentTurnPlayerIndex;
        }

        /// <summary>
        /// 下一回合
        /// </summary>
        public void NextTurn()
        {
            currentTurnPlayerIndex = (currentTurnPlayerIndex + 1) % teamPlayers.Count;
        }

        /// <summary>
        /// 获得当前队伍人数
        /// </summary>
        /// <returns></returns>
        public int GetCurrentTeamPlayerCount()
        {
            return teamPlayers.Count;
        }

    }
}

