/*
 Author: JackZhang
 Description: 计时类
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBaseUtil
{
    public class BaseTimer : MonoBehaviour
    {
        //计时完成后的回调
        private Action timerCallback;

        //计时的时长
        private float timerDuration;

        /// <summary>
        /// 计时器初始化，设置时长和完成后的回调
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="callback"></param>
        public void SetTimer(float duration, Action callback)
        {
            timerDuration = duration;
            timerCallback = callback;
        }

        private void Update()
        {
            if(timerDuration > 0f)
            {
                timerDuration -= Time.deltaTime;
                if (IsTimerComplete())
                {
                    timerCallback();
                }
            }
        }

        private bool IsTimerComplete()
        {
            return timerDuration <= 0;
        }
    }
}

