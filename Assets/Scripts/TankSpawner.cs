using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tanks;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnTime = 3f;

    private void Start()
    {
        StartCoroutine(SpawnTank());
    }

    
    IEnumerator SpawnTank()
    {
        while (true)
        {
            int limit;
            if (Stats.HurdLevel < _tanks.Count)
            {
                limit = Stats.HurdLevel;
            }
            else
            {
                limit = _tanks.Count;
            }

            Instantiate(_tanks[Random.Range(0, limit)], _spawnPoints[Random.Range(0, _spawnPoints.Count)].position,
                Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}