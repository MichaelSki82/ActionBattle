using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ShootableTank : BaseTank
{
    [SerializeField] private string _projectileTag;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] protected float _reloadTime = 0.5f;
    [SerializeField] private AudioSource _firingTankSound;
    private ObjectPooler _objectPooler;
    

    protected override void Start()
    {
        base.Start();
        _objectPooler = ObjectPooler.Instance;
    }

    protected void Shoot()
    {
        _objectPooler.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation);
        _firingTankSound.Play();
    }
}