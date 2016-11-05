using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyWhenParticlesEmpty : MonoBehaviour
{
    public float gracePeriod = 1f;

    ParticleSystem ps;
    float t0;

    public void Start()
    {
        t0 = Time.time;
        ps = GetComponent<ParticleSystem>();
    }

	public void Update ()
    {
        if (Time.time - t0 > gracePeriod && ps.particleCount == 0)
            Destroy(gameObject);
	}
}
