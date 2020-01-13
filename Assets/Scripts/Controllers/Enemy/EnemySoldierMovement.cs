using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldierMovement : MonoBehaviour {

    public float startingMovementSpeed = 1f;
    public float movementSpeed = 1f;
    public float targetIsCloseMS = 0.2f;
    public float stopRange = 2f;

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        movementSpeed = startingMovementSpeed;
        rb = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(rb.transform.position.y <-4.2f)
        rb.velocity = transform.right * FirstTarget();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            Debug.LogError("No effect");
            Transform collision = collider.transform;
            Physics2D.IgnoreCollision(collider.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>());
        }
    }

    float FirstTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stopRange);
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in colliders)
        {
            if (potentialTarget.tag == "Player" || potentialTarget.tag == "Loot")
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
            return movementSpeed;
    }
}
