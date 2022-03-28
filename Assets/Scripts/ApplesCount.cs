using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ApplesCount : MonoBehaviour
{
    private Text _text;
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (_text.text != Convert.ToString(PlayerPrefs.GetInt("Apples")))
        {
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        _text.DOFade(0.5f, 0.2f);
        transform.DOScale(new Vector3(0.95f, 0.95f, 1), 0.2f);
        yield return new WaitForSeconds(0.2f);
        _text.text = Convert.ToString(PlayerPrefs.GetInt("Apples"));
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        _text.DOFade(1, 0.2f);
    }
}
