using UnityEngine;

public class MedicineAction : MonoBehaviour
{
    [SerializeField] private float _healthPoints = 15f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerTank>())
        {
            collision.gameObject.GetComponent<BaseTank>().HealthUpdate(_healthPoints);
            gameObject.SetActive(false);
                
        }
    }
}
