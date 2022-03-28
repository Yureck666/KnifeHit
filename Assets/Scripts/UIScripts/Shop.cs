using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private Blade[] knifes;
    private int _chosenKnife;
    private Vector3 _scale;
    [SerializeField] private RectTransform closePosition;
    [SerializeField] private Image menuKnife;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Button equipButton;
    [SerializeField] private Text equipButtonText;
    private Vector3 _position;
    private void Awake()
    {
        _position = transform.position;
        knifes = levelManager.GetKnifes();
        _scale = menuKnife.transform.localScale;
        if (!PlayerPrefs.HasKey("Knife"))
        {
            PlayerPrefs.SetInt("Knife", 0);
        }
    }

    private void Start()
    {
        _chosenKnife = PlayerPrefs.GetInt("Knife");
        menuKnife.sprite = levelManager.GetKnifes()[_chosenKnife].GetComponent<SpriteRenderer>().sprite;
        CheckEquip();
    }

    private void ShowBlade(int knifeID)
    {
        
        CheckEquip();
        menuKnife.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            menuKnife.sprite = knifes[knifeID].GetComponent<SpriteRenderer>().sprite;
            menuKnife.transform.DOScale(_scale, 0.2f);
        });
    }

    private void CheckEquip()
    {
        if (PlayerPrefs.GetInt("Knife") == _chosenKnife)
        {
            equipButton.interactable = false;
            equipButtonText.text = "Selected";
        }
        else
        {
            equipButton.interactable = true;
            equipButtonText.text = "Select";
        }
    }

    public void SelectButton(bool rigth)
    {
        if (rigth)
        {
            _chosenKnife += 1;
            if (_chosenKnife > knifes.Length-1)
            {
                _chosenKnife = 0;
            }
        }
        else
        {
            _chosenKnife -= 1;
            if (_chosenKnife < 0)
            {
                _chosenKnife = knifes.Length - 1;
            }
        }
        ShowBlade(_chosenKnife);
    }

    public void EquipButton()
    {
        PlayerPrefs.SetInt("Knife", _chosenKnife);
        CheckEquip();
    }

    public void Close()
    {
        transform.DOMove(closePosition.position, 1);
    }

    public void Open()
    {
        transform.DOMove(_position, 1);
    }
}
