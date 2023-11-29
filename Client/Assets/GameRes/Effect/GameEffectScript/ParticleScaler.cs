using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//[ExecuteInEditMode]
public class ParticleScaler : MonoBehaviour
{
    private ParticleSystem[] _particleSystems;
    private TrailRenderer[] _trailRenderers;
    
    public float particleScale = 1.0f;
    public bool alsoScaleGameobject = true;

    private float prevParticleScale = 1.0f;

    private void Start()
    {
        if (_particleSystems == null)
            _particleSystems = GetComponentsInChildren<ParticleSystem>(true);

        foreach (var system in _particleSystems)
        {
            system.Clear();
        }
    }

    public void SetScale(float scale)
    {
        particleScale = scale;
    }

    public void Reset()
    {
        prevParticleScale = 1.0f;
        particleScale = 1.0f;
        alsoScaleGameobject = true;

        Start();
    }

    private void Update()
    {
        if (particleScale > 0 && Mathf.Abs(prevParticleScale - particleScale) > 0.01f)
        {
            if (alsoScaleGameobject)
                transform.localScale = new Vector3(particleScale, particleScale, particleScale);

            float scaleFactor = particleScale / prevParticleScale;

            ScaleShurikenSystems(scaleFactor);
            ScaleTrailRenderers(scaleFactor);

            prevParticleScale = particleScale;
        }
    }

    private void ScaleShurikenSystems(float scaleFactor)
    {
        if (_particleSystems == null)
            _particleSystems = GetComponentsInChildren<ParticleSystem>(true);

        foreach (var ps in _particleSystems)
        {
            var main = ps.main;
            main.startSpeedMultiplier *= scaleFactor;
            main.startSizeMultiplier *= scaleFactor;
            main.gravityModifierMultiplier *= scaleFactor;

            // Scale other modules as needed, example:
            // var velocity = ps.velocityOverLifetime;
            // velocity.xMultiplier *= scaleFactor;
            // velocity.yMultiplier *= scaleFactor;
            // velocity.zMultiplier *= scaleFactor;
            
            // Add additional module scaling here as needed
        }
    }

    private void ScaleTrailRenderers(float scaleFactor)
    {
        if (_trailRenderers == null)
            _trailRenderers = GetComponentsInChildren<TrailRenderer>(true);

        foreach (var trail in _trailRenderers)
        {
            trail.startWidth *= scaleFactor;
            trail.endWidth *= scaleFactor;
        }
    }
}
