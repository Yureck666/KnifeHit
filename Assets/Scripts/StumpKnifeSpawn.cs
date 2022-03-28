using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class StumpKnifeSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject knife;

    public void SpawnKnifes()
    {
        int[] spawned = new int[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int _curRand = Random.Range(1, spawnPoints.Length+1);
            if (!spawned.Contains(_curRand))
            {
                GameObject _curknife = Instantiate(knife, spawnPoints[_curRand-1].position, spawnPoints[_curRand-1].rotation);
                _curknife.transform.parent = transform;
                spawned[i] = _curRand;
                Color _color = _curknife.GetComponent<SpriteRenderer>().color;
                _color.a = 0;
                _curknife.GetComponent<SpriteRenderer>().color = _color;
                _curknife.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
            }
            
        }
    }
}
