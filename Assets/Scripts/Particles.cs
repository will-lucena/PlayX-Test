using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;

    private void OnEnable()
    {
        TreeController.initParticles += init;
    }

    private void OnDisable()
    {
        TreeController.initParticles -= init;
    }

    public void init(Vector3 position)
    {
        Instantiate(particlePrefab).transform.localPosition = new Vector3(position.x, 0, position.z);
    }
}
