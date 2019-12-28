using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    private ParticleSystem particles;
    private Joint _joint;

    private void Awake()
    {
        _joint = GetComponent<FixedJoint>();
        //particles = GetComponent<ParticleSystem>();
        //particles.Stop();
    }

    public void callParticles()
    {
        if (particles.isEmitting)
        {
            particles.Stop();
        }
        particles.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            //RoundManager.callParticles?.Invoke();
        }
    }

    public void linkToTrunkBelow(GameObject trunk)
    {
        _joint.connectedBody = trunk.GetComponent<Rigidbody>();
    }
}
