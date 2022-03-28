using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class StumpDeath : MonoBehaviour
{
    [SerializeField] private GameObject _mainStump;
    

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider2D;
    private Animator _animator;
    void Awake()
    {
        _spriteRenderer = _mainStump.GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        
        LevelManager.FinishEvent.AddListener(() =>
            {
            StartCoroutine(WinDeath());
            });
        LevelManager.LoseGameEvent.AddListener(() =>
            {
                StartCoroutine(LoseDeath());
            });
    }

    private IEnumerator WinDeath()
    {
        _spriteRenderer.DOFade(0, 0.1f);
        _circleCollider2D.enabled = false;
        _animator.enabled = false;
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<BoxCollider2D>(out var _boxCollider))
            {
                _boxCollider.enabled = false;
            }

            if (child.TryGetComponent<Rigidbody2D>(out var currentRigidbody))
            {
                currentRigidbody.bodyType = RigidbodyType2D.Dynamic;
                currentRigidbody.gravityScale = 1;
                Vector2 direction = new Vector2(Random.Range(-5, 5), Random.Range(4, 6));
                currentRigidbody.velocity = direction;
                currentRigidbody.angularVelocity = Random.Range(-360, 360);
            }
        }
        yield return new WaitForSeconds(0.5f);
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<SpriteRenderer>(out var currentSpriteRenderer))
            {
                currentSpriteRenderer.DOFade(0, 0.5f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        LevelManager.StumpDestroyEvent.Invoke(true);
    }

    public IEnumerator LoseDeath()
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.DOFade(0, 0.5f);
        foreach (Transform child in transform)
        {
            SpriteRenderer currentSpriteRenderer;
            if (child.TryGetComponent<SpriteRenderer>(out currentSpriteRenderer))
            {
                currentSpriteRenderer.DOFade(0, 0.5f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        LevelManager.StumpDestroyEvent.Invoke(false);
    }
}
