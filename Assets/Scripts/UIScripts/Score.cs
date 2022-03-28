using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private RectTransform closePosition;
    [SerializeField] private Text score;
    [SerializeField] private Text best;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject newBest;
    
    private Vector3 _position;
    private void Awake()
    {
        gameObject.SetActive(true);
        _position = transform.position;
        transform.position = closePosition.position;
        newBest.SetActive(false);
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
        transform.DOMove(closePosition.position, 0.5f);
    }
    
    private void Open()
    {
        score.text = Convert.ToString(_levelManager.GetScore());
        best.text = Convert.ToString(PlayerPrefs.GetInt("BestScore"));
        if (_levelManager.GetNewBest())
        {
            newBest.SetActive(true);
        }
        else
        {
            newBest.SetActive(false);
        }
        transform.DOMove(_position, 1);
    }
}
