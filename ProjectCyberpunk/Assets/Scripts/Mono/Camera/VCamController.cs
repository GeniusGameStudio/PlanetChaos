using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    private float mousewheel;

    public float scaleRate = 0.99f;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousewheel = Input.GetAxis("Mouse ScrollWheel");

        if (mousewheel < 0)
            vcam.m_Lens.OrthographicSize /= scaleRate;
        else if (mousewheel > 0)
            vcam.m_Lens.OrthographicSize *= scaleRate;

        vcam.m_Lens.OrthographicSize = Mathf.Clamp(vcam.m_Lens.OrthographicSize, 5f, 7.159864f);
    }
}
