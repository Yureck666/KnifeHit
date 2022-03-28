using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StumpSpawnApple : MonoBehaviour
{
    [SerializeField] private SOAppleChanсe soAppleChanсe;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject applePrefab;
    
    public void SpawnApple()
    {
        if (Random.Range(1, 100) < soAppleChanсe.Chance)
        {
            GameObject apple = Instantiate(applePrefab, spawnPoint.position, Quaternion.identity, transform);
            Color _color = apple.GetComponent<SpriteRenderer>().color;
            _color.a = 0;
            apple.GetComponent<SpriteRenderer>().color = _color;
            apple.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
        }
    }
}
