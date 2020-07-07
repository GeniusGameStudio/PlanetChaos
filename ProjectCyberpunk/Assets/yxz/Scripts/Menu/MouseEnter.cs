using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//鼠标指向按钮有放大效果
public class MouseEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
