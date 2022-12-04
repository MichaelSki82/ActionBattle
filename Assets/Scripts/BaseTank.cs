using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseTank : MonoBehaviour
{
    //public event Action 
    
    [SerializeField] private int _maxhealth = 50;
    [Range(1f, 5f)] [SerializeField] protected float _movementSpeed = 3f;

    [SerializeField] protected float _angleOffset = 90f;
    [SerializeField] protected float _rotationSpeed = 7f;
    [Space(10)] [SerializeField] private int _points = 0;
    protected Rigidbody2D _rigidbody;
    protected int _currentHealth;
    protected GameUI _gameUI;

    protected virtual void Start()
    {
        _currentHealth = _maxhealth;
        _rigidbody = GetComponent<Rigidbody2D>();
        _gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        _gameUI.RetryButtonPressed += OnRetryButtonPressed;
        _gameUI.NewGameButtonPressed += OnNewGameButtonPressed;
    }

    private  void OnNewGameButtonPressed()
    {
        _gameUI.RestartGame();
        _gameUI.SetVictoryWindow(false);
    }

    private void OnRetryButtonPressed()
    {
        _gameUI.RestartGame();
        _gameUI.SetLoseWindow(false);
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Stats.Score += _points;
            _gameUI.UpdateScoreAndLevel();
            Destroy(gameObject);
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.up * _movementSpeed * Time.deltaTime);
    }

    protected void SetAngle(Vector3 target)
    {
        transform.LookAt(target, Vector3.forward);
    }

    private void OnDisable()
    {
        _gameUI.RetryButtonPressed -= OnRetryButtonPressed;
        _gameUI.NewGameButtonPressed -= OnNewGameButtonPressed;
    }
}