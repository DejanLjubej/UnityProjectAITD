using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimationDeactivate : MonoBehaviour {

    public ParticleSystem particleSystem;

    float timeTillDeactivate;
    float timeActive;
	// Use this for initialization
	void Start () {
        timeTillDeactivate = particleSystem.duration;
	}
	
	// Update is called once per frame
	void Update () {

        timeActive += Time.deltaTime;

        if (timeActive >= timeTillDeactivate)
        {
            this.gameObject.SetActive(false);
            timeActive =0;
        }
	}
}
