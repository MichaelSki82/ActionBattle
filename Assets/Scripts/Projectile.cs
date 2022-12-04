using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _speed = 5;
        [SerializeField] private string _myTag = "";


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<BaseTank>() != null && collision.gameObject.tag != _myTag)
            {
                collision.gameObject.GetComponent<BaseTank>().TakeDamage(_damage);
                gameObject.SetActive(false);
                
            }
        }

        private void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }
    }

