using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StumpRotation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed1, float speed2)
    {
        _animator.SetFloat("Scale1", speed1);
        _animator.SetFloat("Scale2", speed2);
    }
}
