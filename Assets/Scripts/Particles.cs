using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;

    private Queue<GameObject> _particles;
    private void OnEnable()
    {
        TreeController.initParticles += init;
        RoundController.initRound += cleanParticles;
    }

    private void OnDisable()
    {
        TreeController.initParticles -= init;
        RoundController.initRound -= cleanParticles;
    }

    private void Start()
    {
        _particles = new Queue<GameObject>();
    }

    public void init(Vector3 position)
    {
        ParticleSystem particles = Instantiate(particlePrefab);
        particles.transform.localPosition = new Vector3(position.x, 0, position.z);
        _particles.Enqueue(particles.gameObject);
        StartCoroutine(checkSimulationEnd(particles));
    }
    
    private IEnumerator checkSimulationEnd(ParticleSystem _particleSystem)
    {
        while (_particleSystem.IsAlive())
        {
            yield return null;
        }
        Destroy(_particleSystem.gameObject);
    }

    private void cleanParticles()
    {
        StopAllCoroutines();
        while (_particles.Count > 0)
        {
            Destroy(_particles.Dequeue());
        }
    }
}
