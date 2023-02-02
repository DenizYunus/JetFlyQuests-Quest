using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    List<ParticleSystem> particles;

    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>().ToList();
        particles.ForEach(x => x.Stop());
    }

    public void EnableParticles()
    {
        particles.ForEach(x => x.Play());
    }

    public void DisableParticles()
    {
        particles.ForEach(x => x.Stop());
    }
}
