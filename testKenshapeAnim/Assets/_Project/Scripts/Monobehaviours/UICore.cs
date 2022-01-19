using System;
using BombGame;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class UICore : MonoBehaviour
{
    private void OnEnable()
    {
        CEvents.OnTimerChanged += RefreshTimer;
        CEvents.OnEnemyKilled += AddScore;
        CEvents.OnLoseGame += LoseGame;
        CEvents.OnWinGame += WinGame;
    }

    public EcsEntity Control;

    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _finishText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private GameObject[] _menus;
    private int _score;
    private bool _win;

    private void Awake()
    {
        _menus = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _menus[i] = transform.GetChild(i).gameObject;
        }

        SwapMenu(0);
    }

    public void SwapMenu(int index)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (i != index)
            {
                _menus[i].SetActive(false);
            }
            else
            {
                _menus[i].SetActive(true);
            }
        }
    }

    public void AddState(int index)
    {
        switch (index)
        {
            case 0:
            {
                Control.Del<Win>();
                Control.Del<Lose>();
                Control.Get<MainMenuScreen>();
                break;
            }
            case 1:
            {   
                Control.Del<Win>();
                Control.Del<Lose>();
                Control.Get<GameplayScreen>();
                break;
            }
            case 2:
            {
                PassScore(false);
                SwapMenu(2);
                Control.Del<Win>();
                Control.Del<Lose>();
                Control.Get<ShopScreen>();
                break;
            }
            case 3:
            {
                Control.Get<FinishScreen>();
                break;
            }
            case 4: // x3
            {
                PassScore(true);
                SwapMenu(0);
                Control.Del<Win>();
                Control.Del<Lose>();
                Control.Get<MainMenuScreen>();
                break;
            }
        }
    }

    private void AddScore()
    {
        _score++;
    }

    private void LoseGame(int level)
    {
        _finishText.SetText($"You Lost!");
        _score = _score * level;
        _scoreText.SetText($"{_score}$");
        Control.Get<FinishScreen>();

        SwapMenu(3);
    }

    private void WinGame(int level)
    {
        _finishText.SetText($"You Win!");
        _score = _score * 2 * level;
        _scoreText.SetText($"{_score}$");
        Control.Get<FinishScreen>();

        SwapMenu(3);
    }

    private void RefreshTimer(string time)
    {
        _timer.SetText(time);
    }

    public void PassScore(bool x3)
    {
        if (x3)
        {
            Control.Get<AddScore>().Score = _score * 3;
            Control.Get<IncreaseLevel>();
        }
        else
        {
            Control.Get<AddScore>().Score = _score;
        }
        
        _score = 0;
    }

    private void OnDisable()
    {
        CEvents.OnTimerChanged -= RefreshTimer;
        CEvents.OnEnemyKilled += AddScore;
        CEvents.OnLoseGame += LoseGame;
        CEvents.OnWinGame += WinGame;
    }
}