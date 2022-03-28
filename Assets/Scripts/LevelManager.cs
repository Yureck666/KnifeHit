using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static UnityEvent FinishEvent;
    public static UnityEvent LoseGameEvent;
    public static UnityEvent AddApple;
    public static UnityEvent<bool> StumpDestroyEvent;

    [SerializeField] private Blade[] knifesList;
    [SerializeField] private KnifeBar knifeBar;
    [SerializeField] private int minBossOften;
    [SerializeField] private int maxBossOften;
    [SerializeField] private int minHP;
    [SerializeField] private int maxHP;
    [SerializeField] private StumpSystem stumpSystem;
    [SerializeField] private Stump stumpUsual;
    [SerializeField] private Stump stumpBoss;

    private StumpSystem currentStump;
    private int _currentBossOften;
    private int _score;
    private int _nextBoss;
    private bool _newBest;
    private void Awake()
    {
        FinishEvent = new UnityEvent();
        LoseGameEvent = new UnityEvent();
        AddApple = new UnityEvent();
        StumpDestroyEvent = new UnityEvent<bool>();

        SetNewBoss();
        
        if (!PlayerPrefs.HasKey("Apples"))
        {
            PlayerPrefs.SetInt("Apples", 0);
        }
        Vibration.Init();
        FinishEvent.AddListener(() =>
        {
            AddScore(1);
            SetNewBoss();
            Vibration.VibratePeek();
        });
        LoseGameEvent.AddListener(() =>
        {
            SetBest(_score);
            Vibration.VibratePeek();
        });
        AddApple.AddListener(() =>
        {
            PlayerPrefs.SetInt("Apples", PlayerPrefs.GetInt("Apples")+1);
        });
        StumpDestroyEvent.AddListener(win =>
        {
            if (win)
            {
                NextLevel();
            }

            if (!win)
            {
                EndGame();
            }
        });
    }

    private void AddScore(int i)
    {
        _score += i;
    }
    private void SetNewBoss()
    {
        if ((_score - 1 == _nextBoss) || (_score == 0))
        {
            _currentBossOften = Random.Range(minBossOften, maxBossOften);
            _nextBoss = _score + _currentBossOften;
        }
    }
    
    private void SetBest(int score)
    {
        if (PlayerPrefs.GetInt("BestScore") < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
            _newBest = true;
        }
    }
    
    private void EndGame()
    {
        Destroy(currentStump.gameObject);
    }
    private void NextLevel()
    {
        Stump nextStump;
        float speed1 = (float)(1 + (Random.Range(1, 10) * 0.005 * _score));
        float speed2 = (float)(1 + (Random.Range(1, 10) * 0.005 * _score));
        int stumpHP = Random.Range(minHP, maxHP);
        RefreshKnifeBar(stumpHP);
        if (_score == _nextBoss)
        {
            nextStump = stumpBoss;
        }
        else
        {
            nextStump = stumpUsual;
        }
        currentStump.SpawnStump(new StumpParameters(){Stump = nextStump, Blade = knifesList[PlayerPrefs.GetInt("Knife")], HP = stumpHP, Speed1 = speed1, Speed2 = speed2});
    }
    
    public void StartGame()
    {
        _newBest = false;
        _score = 0;
        SetNewBoss();
        currentStump = Instantiate(stumpSystem.gameObject).GetComponent<StumpSystem>();
        currentStump.Init(this);
        NextLevel();
    }

    public void RefreshKnifeBar(int HP)
    {
        knifeBar.RefreshBar(HP);
    }
    
    public int GetScore()
    {
        return _score;
    }

    public bool GetNewBest()
    {
        return _newBest;
    }

    public Blade[] GetKnifes()
    {
        return knifesList;
    }

    public int GetBossOften()
    {
        return _currentBossOften+1;
    }

    public int GetProgress()
    {
        return _currentBossOften - (_nextBoss - _score);
    }

    public int GetNextBoss()
    {
        return _nextBoss;
    }
}
