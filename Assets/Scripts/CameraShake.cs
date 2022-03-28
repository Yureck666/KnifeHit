using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float loseShakeTime;
    [SerializeField] private float winShakeTime;

    private Vector3 _camPos;
    private void Start()
    {
        _camPos = transform.position;
        LevelManager.LoseGameEvent.AddListener(() =>
        {
            Shake(loseShakeTime);
        });
        LevelManager.FinishEvent.AddListener(() =>
        {
            Shake(winShakeTime);
        });
    }

    public void Shake(float time)
    {
        transform.DOShakePosition(time, 1, Convert.ToInt32(500 * time))
            .OnComplete(() => transform.DOMove(_camPos, time / 4));
    }
}
