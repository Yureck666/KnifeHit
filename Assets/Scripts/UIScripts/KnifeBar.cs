using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class KnifeBar : MonoBehaviour
{
    [SerializeField] private Image[] knifes;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        LevelManager.LoseGameEvent.AddListener(Close);
    }

    public void Open()
    {
        for (int i = 0; i < knifes.Length; i++)
        {
            Color _color = knifes[i].color;
            _color.a = 0;
            knifes[i].color = _color;
        }
        _canvasGroup.DOFade(1, 0.2f);
    }

    private void Close()
    {
        _canvasGroup.DOFade(0, 0.2f);
    }
    
    public void RefreshBar(int HP)
    {
        for (int i = 0; i < knifes.Length; i++)
        {
            if (HP > i)
            {
                if (knifes[i].color.a != 1)
                {
                    knifes[i].DOFade(1, 0.2f);
                }
            }
            else
            {
                if (knifes[i].color.a != 0)
                {
                    knifes[i].DOFade(0, 0.2f);
                }
            }
        }
    }
}
