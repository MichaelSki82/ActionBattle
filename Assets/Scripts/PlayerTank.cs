using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTank : ShootableTank
{
    private float _timer;
    
    
    protected override void Start()
    {
        base.Start();
        _gameUI.RetryButtonPressed += GameUIOnRetryButtonPressed;
        _gameUI.NextLevelButtonPressed += GameUIOnNextLevelButtonPressed;
        _gameUI.ResturtGameButtonPressed += GameUIOnResturtButtonPressed;
        Stats.ShowVictoryWin += VictoryWindowShow;
    }

    private void VictoryWindowShow()
    {
        Time.timeScale = 0f;
        _gameUI.SetVictoryWindow(true);
    }

    private void GameUIOnResturtButtonPressed()
    {
        _gameUI.ButtonSoundPlay();
        _gameUI.SetVictoryWindow(false);
        Time.timeScale = 1f;
        _gameUI._gameIsPoused = false;
        Stats.ResetAllStats();
        SceneManager.LoadScene(0);
    }

    private void GameUIOnNextLevelButtonPressed()
    {
        
        _gameUI.SetVictoryWindow(false);
        Time.timeScale = 1f;
        _gameUI._gameIsPoused = false;
        Stats.HurdLevel++;
        Stats.Score = 0;
    }


    private void GameUIOnRetryButtonPressed()
    {
       _gameUI.ButtonSoundPlay();
       _gameUI.SetLoseWindow(false);
       Time.timeScale = 1f;
       _gameUI._gameIsPoused = false;
       Stats.ResetAllStats();
       SceneManager.LoadScene(0);
       
    }
    
    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _gameUI.UpdateHp(_currentHealth);
        if (_currentHealth <= 0)
        {
            Time.timeScale = 0f;
            _gameUI.SetLoseWindow(true);
        }
    }

   

    protected override void Move()
    {
        transform.Translate(Vector2.right * Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
        SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (_timer <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                _timer = _reloadTime;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }

        
    }

//     private void OnDisable()
//     {
//         _gameUI.RetryButtonPressed -= GameUIOnRetryButtonPressed;
//         _gameUI.NextLevelButtonPressed -= GameUIOnNextLevelButtonPressed;
//         _gameUI.ResturtGameButtonPressed -= GameUIOnResturtButtonPressed;
//     }   
//
}