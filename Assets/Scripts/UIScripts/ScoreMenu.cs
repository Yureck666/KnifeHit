using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private RectTransform closePosition;

    
    private Vector3 _position;

    private void Awake()
    {
        gameObject.SetActive(true);
        _position = transform.position;
        transform.position = closePosition.position;
    }

    private void Start()
    {
        LevelManager.StumpDestroyEvent.AddListener(win =>
        {
            if (!win)
            {
                Open();
            }
        });
    }

    public void Close()
    {
        transform.DOMove(closePosition.position, 1);
    }
    
    private void Open()
    {
        transform.DOMove(_position, 1);
    }
}
