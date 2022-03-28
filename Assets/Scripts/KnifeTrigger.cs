using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class KnifeTrigger : MonoBehaviour
{
    private Boolean _trigger = true;
    
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        
        LevelManager.LoseGameEvent.AddListener(() =>
        {
            if (_trigger)
            {
                StartCoroutine(DestroyKnife());
            }
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_trigger)
        {
            if (collision.gameObject.TryGetComponent<Knife>(out _))
            {
                StartCoroutine(HitTheKnife(collision.transform));
            }
        }
    }

    public void CatchKnife(Transform stump)
    {
        _boxCollider2D.enabled = false;
        _trigger = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = stump;
    }
    
    private IEnumerator HitTheKnife(Transform colKnife)
    {
        LevelManager.LoseGameEvent.Invoke();
        GetComponent<BoxCollider2D>().enabled = false;
        Rigidbody2D _rig = GetComponent<Rigidbody2D>();
        _rig.DORotate(Random.Range(-180, 180), 1);
        _rig.velocity = (transform.position - colKnife.position)*5;
        if (Random.Range(0, 2) == 0)
        {
            _rig.angularVelocity = Random.Range(-270, -180);
        }
        else
        {
            _rig.angularVelocity = Random.Range(180, 270);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DestroyKnife());
    }

    private IEnumerator DestroyKnife()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Apple>(out _))
        {
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
            LevelManager.AddApple.Invoke();
        }
    }
}
