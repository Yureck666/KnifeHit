using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject progressPoint;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Color progressColor;
    [SerializeField] private Color bossColor;
    [SerializeField] private Vector3 deltaScale;

    private CanvasGroup _canvasGroup;
    private GameObject[] points;
    private int _nextBoss;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void Start()
    {
        LevelManager.StumpDestroyEvent.AddListener(win =>
        {
            if (win)
            {
                SetProgressBar();
            }
            else
            {
                Close();
            }
        });
    }

    public void Open()
    {
        _canvasGroup.DOFade(1, 0.5f);
        SetProgressBar();
    }

    private void Close()
    {
        _canvasGroup.DOFade(0, 0.5f);
    }
    private void SetProgressBar()
    {
        int pointCount = levelManager.GetBossOften();
        int nextBoss = levelManager.GetNextBoss();
        if (_nextBoss != nextBoss)
        {
            _nextBoss = nextBoss;
            if (transform.childCount != 0)
            {
                foreach (GameObject point in points)
                {
                    Destroy(point);
                }
            }
            points = new GameObject[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                points[i] = Instantiate(progressPoint, transform);
            }
        }
        for (int i = 0; i < levelManager.GetProgress(); i++)
        {
            transform.DOScale(transform.localScale - deltaScale, 0.2f)
                .OnComplete(() => transform.DOScale(transform.localScale + deltaScale, 0.3f));
            points[i].GetComponent<Image>().DOColor(progressColor, 0.5f);
        }

        points[pointCount - 1].GetComponent<Image>().color = bossColor;
    }
}