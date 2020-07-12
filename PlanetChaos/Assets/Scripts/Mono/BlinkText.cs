using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    Text text;
    public int internalTime;

    void Start()
    {
        text = GetComponent<Text>();
        InvokeRepeating("FadeText", 0, internalTime * 2);
    }

    private void FadeText()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(text.DOFade(0, internalTime));
        sequence.Append(text.DOFade(1, internalTime));
    }
}
