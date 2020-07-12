using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip click;

    private AudioSource SFX;

    private void Start()
    {
        SFX = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SFX.PlayOneShot(click);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f, 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.5f).SetUpdate(true);
    }



}
