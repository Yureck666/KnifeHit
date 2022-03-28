using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KnifeShoot : MonoBehaviour
{
    public float Force; 
    
    public Boolean _curBlade = true;

    private void Awake()
    {
        InputCatcher.TapEvent.AddListener(Shoot);
    }


    
    private void Shoot()
    {
        if (_curBlade)
        {
            GetComponent<Rigidbody2D>().velocity = (Vector2.up * Force);
            _curBlade = false;
        }
    }
}
