using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class BaseTank : MonoBehaviour
{
    
            [SerializeField] private int _maxhealth = 30;
            [Range(1f, 5f)]
            [SerializeField] protected float _movementSpeed = 3f;
           
            [SerializeField] protected float _angleOffset = 90f;
            [SerializeField] protected float _rotationSpeed = 7f;
            [Space(10)]
            [SerializeField] private int _points = 0;
            protected Rigidbody2D _rigidbody;
            protected int _currentHealth;
            protected GameUI _gameUI;
    
            protected virtual void Start()
            {
                _currentHealth = _maxhealth;
                _rigidbody = GetComponent<Rigidbody2D>();
                _gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
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
}
