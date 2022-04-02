using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRunner : MonoBehaviour
{
    //[SerializeField] private GameObject particles;

    public void RunParticles([SerializeField] ParticleSystem particles)
    {
        Instantiate(particles,Vector3.zero, Quaternion.identity, gameObject.transform);
    }
}
