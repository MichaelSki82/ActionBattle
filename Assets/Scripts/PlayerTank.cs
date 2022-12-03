using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTank : ShootableTank
{
    private float _timer;

    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _gameUI.UpdateHp(_currentHealth);
        if(_currentHealth <=0)
        {
            Stats.ResetAllStats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
    protected override void Move()
    {
        transform.Translate(Vector2.up * Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime);
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
}

