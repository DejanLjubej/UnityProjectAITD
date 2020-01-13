using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisPush : MonoBehaviour {

    public float push = 0f;

    Rigidbody2D debrisRB;
    ParticleSystem dPs;
	// Use this for initialization
	void Start () {
        debrisRB = GetComponent<Rigidbody2D>();
        dPs = GetComponent<ParticleSystem>();
        debrisRB.velocity = transform.right * push;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(debrisRB.transform.rotation.y != 0)
        {
            Debug.Log(dPs.shape);
            //dPs.shape.rotation.y = debrisRB.transform.rotation.y;
        }
	}
}
