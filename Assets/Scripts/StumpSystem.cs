using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpSystem : MonoBehaviour
{
    [SerializeField] private Transform stumpSpawnPoint;

    public LevelManager LevelManager_;

    private int _lifes;

    public void Init(LevelManager lvlMngr)
    {
        LevelManager_ = lvlMngr;
    }
    public void SpawnStump(StumpParameters stumpParameters)
    {
        GameObject _curStump = Instantiate(stumpParameters.Stump.gameObject, stumpSpawnPoint.position, Quaternion.identity);
        _curStump.GetComponent<StumpTrigger>().Init(this, stumpParameters.Blade);
        _curStump.GetComponent<StumpRotation>().SetSpeed(stumpParameters.Speed1, stumpParameters.Speed2);
        _lifes = stumpParameters.HP;
    }
    
    public int GetHP()
    {
        return _lifes;
    }
    
    public void Damage(int damage)
    {
        _lifes -= damage;
        LevelManager_.RefreshKnifeBar(_lifes);
    }
}
