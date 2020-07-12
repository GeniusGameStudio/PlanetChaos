using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //场景状态
    SceneStateController m_SceneStateController = new SceneStateController();

    //BGM
    public AudioClip start;
    public AudioClip battleGame;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        m_SceneStateController.SetState(new StartState(m_SceneStateController), "");
    }

    // Update is called once per frame
    void Update()
    {
        m_SceneStateController.StateUpdate();
    }
}
