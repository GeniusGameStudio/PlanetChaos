using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    //场景状态
    private ISceneState m_State;

    //是否开始
    private bool m_bRunBegin = false;

    //场景加载判断
    private AsyncOperation m_AsyncOperation = null;

    public SceneStateController() { }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="State"></param>
    /// <param name="LoadLevelName"></param>
    public void SetState(ISceneState State, string LoadLevelName)
    {
        Debug.Log("SetState:" + State.ToString());

        m_bRunBegin = false;

        //载入场景
        LoadLevel(LoadLevelName);

        //让前一个场景状态结束
        if (m_State != null)
            m_State.StateEnd();

        //设置状态
        m_State = State;
    }

    /// <summary>
    /// 载入场景
    /// </summary>
    /// <param name="LoadLevelName"></param>
    private void LoadLevel(string LoadLevelName)
    {
        if (LoadLevelName == null || LoadLevelName.Length == 0)
            return;
        m_AsyncOperation = SceneManager.LoadSceneAsync(LoadLevelName);
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void StateUpdate()
    {
        //场景是否还在加载
        if (m_AsyncOperation != null)
        {
            if (!m_AsyncOperation.isDone)
            {
                return;
            }
        }

        //通知新的State开始
        if(m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }

        if(m_State != null)
        {
            m_State.StateUpdate();
        }
    }
}
