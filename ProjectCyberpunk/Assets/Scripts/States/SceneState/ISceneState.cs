using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState
{
    //状态的名称
    private string m_StateName = "ISceneState";
    public string StateName
    {
        get
        {
            return m_StateName;
        }

        set
        {
            m_StateName = value;
        }
    }

    //控制者
    protected SceneStateController m_Controller = null;

    //建造者
    public ISceneState(SceneStateController Controller)
    {
        m_Controller = Controller;
    }

    //开始
    public virtual void StateBegin() { }

    //结束
    public virtual void StateEnd() { }

    //更新
    public virtual void StateUpdate() { }

    //重写ToString，打印类型及状态名
    public override string ToString()
    {
        return string.Format("[I_SceneState: StateName={0}]", StateName);
    }
}
