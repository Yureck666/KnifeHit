using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StumpTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 knifeSpawnPoint;
    [SerializeField] private Vector3 deltaScale;
    
    private StumpSystem _stumpSystem;
    private Blade _knife;
    private Boolean GameActive;
    private KnifeBar _knifeBar;
    
    public void Init(StumpSystem stumpSystem,Blade knife)
    {
        _stumpSystem = stumpSystem;
        _knife = knife;
        SpawnNewKnife();
        GameActive = true;
        _knifeBar = GetComponent<KnifeBar>(); 
        LevelManager.LoseGameEvent.AddListener(() =>
        {
            GameActive = false;
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Blade>(out var blade) && GameActive)
        {
            _stumpSystem.Damage(1);
            blade.gameObject.GetComponent<KnifeTrigger>().CatchKnife(transform);
            CatchAnimation();
            Vibration.VibratePop();
            if (_stumpSystem.GetHP() > 0)
            {
                SpawnNewKnife();
            }
            else
            {
                LevelManager.FinishEvent.Invoke();
            }
        }
    }

    private void CatchAnimation()
    {
        Vector3 _scale = transform.localScale;
        Vector3 _position = transform.position;
        transform.DOScale(_scale - deltaScale, 0.1f)
            .OnComplete(() => transform.DOScale(_scale, 0.2f));
        transform.DOMove(_position + Vector3.up * 0.2f, 0.1f)
            .OnComplete(() => transform.DOMove(_position, 0.2f));
    }
    
    private void SpawnNewKnife()
    {
        Instantiate(_knife.gameObject, knifeSpawnPoint, Quaternion.identity);
    }
}