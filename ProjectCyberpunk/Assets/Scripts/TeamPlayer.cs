/*
 Author: JackZhang
 Description: 队伍成员类
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBaseUtil
{
    public class TeamPlayer : MonoBehaviour
    {
        //序号
        private int index;
        public int Index { get { return index; } set { index = value; } }

        public PlayerController PlayerController { get; set; }

        public Team belongsTo { get; set; }

        private bool isDestroyed;

        private PlayerUI ui;

        //血量
        public int hp = 100;

        public void DoHurt(int damage)
        {
            hp -= damage;
            ui.UpdatePlayerHP(hp);
        }

        void Start()
        {
            PlayerController = GetComponent<PlayerController>();
            ui = GetComponent<PlayerUI>();
        }

        void Update()
        {
            if (transform.position.y < -8f)
            {
                if (!isDestroyed)
                {
                    belongsTo.RemoveTeamPlayer(this);
                    if(belongsTo.GetCurrentTeamPlayerCount() > 0)
                    {
                        GameManager.Instance.TurnBaseController.EndTurn();
                        GameManager.Instance.TurnBaseController.StartTurn();
                    }
                    hp = 0;
                    ui.UpdatePlayerHP(hp);
                    belongsTo.UpdateHP();
                    Destroy(gameObject);
                    isDestroyed = true;
                }
                
            }
        }
    }
}

