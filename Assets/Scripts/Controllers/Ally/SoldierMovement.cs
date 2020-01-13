using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour {

    public float soldierSpeed = 1f;
    public float targetIsCloseMS;
    public float stopRange = 2f;
    Rigidbody2D soldierRb;

	// Use this for initialization
	void Awake () {
        soldierRb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        soldierRb.velocity = -transform.right * FirstTarget();
	}
    float FirstTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stopRange);
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in colliders)
        {
            if (potentialTarget.tag == "Enemy" || potentialTarget.tag == "Loot")
            {

                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget.transform;
                }
            }
        }

        if (bestTarget != null)
            return targetIsCloseMS;

        else
            return soldierSpeed;
    }
}
