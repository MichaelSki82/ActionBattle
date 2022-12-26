using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MedicineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _medicine;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnTime = 10f;
    
    private float _currentTime;
    
    private void Start()
    {
        _currentTime = _spawnTime;
    }

    private void Update()
    {
        if (_currentTime <= 0)
        {
            
          Instantiate(_medicine, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position,
                        Quaternion.identity);
          _currentTime = _spawnTime;
        }
        else
        {
            _currentTime -= Time.deltaTime;
        }
        
    }
    
   
      
          
          
       

}
