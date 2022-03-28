using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform closePosition;

    private Vector3 _position;
    private void Awake()
    {
        gameObject.SetActive(true);
        _position = transform.position;
    }

    public void Open()
    {
        transform.DOMove(_position, 1);
    }
    
    public void Close()
    {
        transform.DOMove(closePosition.position, 1);
    }
}
