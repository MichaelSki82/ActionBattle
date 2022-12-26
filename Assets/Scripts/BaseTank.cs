using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseTank : MonoBehaviour
{
    
    [SerializeField] private int _maxhealth = 50;
    [Range(1f, 5f)] [SerializeField] protected float _movementSpeed = 3f;

    [SerializeField] protected float _angleOffset = 90f;
    [SerializeField] protected float _rotationSpeed = 7f;
    [Space(10)] [SerializeField] private int _points = 0;
    protected Rigidbody2D _rigidbody;
    protected float _currentHealth;
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
    
    public virtual void HealthUpdate(float health)
    {
        _currentHealth += health;
        _gameUI.UpdateHp(_currentHealth);

        if (_currentHealth > _maxhealth)
        {
            _currentHealth = _maxhealth;
            _gameUI.UpdateHp(_currentHealth);
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.right * _movementSpeed * Time.deltaTime);
    }

    protected void SetAngle(Vector3 target)
    {
        Vector2 direction = target - transform.position;
        float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angel, Vector3.forward);
        transform.rotation = rotation;
        // target.z = transform.position.z;
        // transform.LookAt(target, Vector3.up);
    }

   
}