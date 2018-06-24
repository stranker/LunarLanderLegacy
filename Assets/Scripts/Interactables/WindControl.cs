using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControl : MonoBehaviour {

    public int windForce;
    public int windDragForce;
    public Vector3 windDirection;
    private ParticleSystem ps;
    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        var vel = ps.velocityOverLifetime;
        vel.space = ParticleSystemSimulationSpace.Local;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 0.0f);
        vel.x = new ParticleSystem.MinMaxCurve(windDirection.x, curve);
        vel.y = new ParticleSystem.MinMaxCurve(windDirection.y, curve);
    }
	
	// Update is called once per frame
	void Update () {
        SetParticlesVelocity(windDirection);
    }

    private void SetParticlesVelocity(Vector3 windDir)
    {

    }

}
