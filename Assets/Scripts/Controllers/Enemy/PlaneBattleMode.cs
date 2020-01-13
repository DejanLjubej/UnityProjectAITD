using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBattleMode : MonoBehaviour
{

    [SerializeField]
    float timer;
    [SerializeField]
    float maxFrequency;
    [SerializeField]
    float minFrequency;
    [SerializeField]
    float lagTimer=0;
    [SerializeField]
    float findTargetRadious;
    public GameObject missile;
    public GameObject dropPoint;

    float frequency;
    public float dropLag = 1;

    public int numOfProjectiles = 1;

    float dropPeriod;

    
    float startBombing;
    ObjectPooling objectPooling;
    // Use this for initialization
    void Start()
    {
        timer = 0;
        frequency = Random.Range(minFrequency, maxFrequency);
        objectPooling = ObjectPooling.Instance;
        dropPeriod = dropLag * numOfProjectiles;

        startBombing = FirstTarget();

        
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
            timer += Time.deltaTime;

        if (timer >= frequency && numOfProjectiles==1)
        {

            //numOfProjectiles = 1;
            timer = 0;
            if(missile!=null)
                    Drop();
               /* if (missile.tag == "EnemyAmmo")
                else if (missile.tag == "Enemy")
                    Parashooter();
                    */
        }
        else if (numOfProjectiles > 1 && timer >= frequency)
        {
                
            if (missile.tag == "EnemyAmmo")
            {
               if(startBombing<= this.transform.position.x)
                InvokeRepeating("Drop", 0.1f, dropLag);

            }
            else if (missile.tag == "Enemy")
            {
                InvokeRepeating("Drop", 0.1f, dropLag);
                //InvokeRepeating("Parashooter", 0.1f, dropLag);
            }

            timer = 0;
        }
        else
        {
            Debug.LogError("Dropn nufn!");
        }
       // lagTimer += Time.deltaTime;
        /*if(timer> dropPeriod )
        {
            Debug.Log("so did i cancel ?" + this.GetInstanceID());
            CancelInvoke();
        }*/
    }

    void Drop()
    {
        lagTimer++;
        if(lagTimer >= numOfProjectiles)
        {
            CancelInvoke();
            lagTimer = 0;
        }
        //Debug.Log("Time3: " + timer);
        if(missile != null)
        { 
            objectPooling.SpawnFromPool(missile.name, dropPoint.transform.position, dropPoint.transform.rotation);
        }
        else
        {
            Debug.LogError("Ney Dropn C:");
        }
        //Debug.Log("Start of invonking");
        //Debug.Log("Time4: " + timer);
        /*if (timer > dropPeriod*1.1)
        {
            Debug.Log("Time5: " + timer);
            CancelInvoke();
            Debug.Log("i'm cancelling");
            Debug.Log("Time6: " + timer);
        }*/
    }

    void Parashooter()
    {
        GameObject deployParashoot = (GameObject)Instantiate(missile, new Vector2(transform.position.x, transform.position.y), transform.rotation);

        //Debug.Log("I was supposede to drop soldiers, but none of them wanted to fight");
    }

    float FirstTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findTargetRadious);
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in colliders)
        {
            if(potentialTarget.tag == "Player" || potentialTarget.tag == "Loot")
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
            return bestTarget.position.x;

        else
            return -4;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findTargetRadious);
    }
}