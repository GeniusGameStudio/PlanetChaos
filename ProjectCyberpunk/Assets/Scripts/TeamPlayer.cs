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

        //血量
        private int hp;
        public int HP { get { return hp; } set { hp = value; } }

        void Start()
        {
            PlayerController = GetComponent<PlayerController>();
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
                    Destroy(gameObject);
                    isDestroyed = true;
                }
                
            }
        }
    }
}

