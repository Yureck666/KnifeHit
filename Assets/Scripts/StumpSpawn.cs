using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StumpSpawn : MonoBehaviour
{
    private Vector3 _scale;
    private void Awake()
    {
        _scale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        transform.DOScale(_scale, 0.2f);
        yield return new WaitForSeconds(0.2f);
        GetComponent<StumpKnifeSpawn>().SpawnKnifes();
        GetComponent<StumpSpawnApple>().SpawnApple();
    }
}
