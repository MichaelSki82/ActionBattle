using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingTank : BaseTank
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private AudioSource _tankTouch;
    private Transform _target;
    private float _timer;
    private float _hitCooldown = 1f;

    protected override void Start()
    {
        base.Start();
        _target = FindObjectOfType<PlayerTank>().transform;
    }

    // protected override void Move()
    // {
    //     transform.Translate(Vector2.up * _movementSpeed * Time.deltaTime);
    //    
    // }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerTank>() != null && _timer <= 0)
        {
            collision.gameObject.GetComponent<PlayerTank>().TakeDamage(_damage);
            _timer = _hitCooldown;
            _tankTouch.Play();
        }
    }

    private void Update()
    {
        if (_target != null)
        {
            if (_timer <= 0)
            {
                Move();
                SetAngle(_target.position);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }
}