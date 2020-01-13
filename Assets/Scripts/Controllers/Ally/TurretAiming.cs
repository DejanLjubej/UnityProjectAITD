using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAiming : MonoBehaviour {

    public GameObject turningAxis;
    public GameObject barrel;
    public GameObject Line;
    public GameObject missile;
    //public Rigidbody2D bullet;

    public float fireRate = 1f;
    public float interpolateSpeed = 1f;
    float firecountdown = 1f;

    public static float explosionHeightY = 0f;


    public static float YAxisOnClick()
    {

        return explosionHeightY;
    }

    ObjectPooling objectPooling;

    // Use this for initialization
    void Start () {
        //Instantiation of misile pool
        objectPooling = ObjectPooling.Instance;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Set the direction of aim based on position of mouse, touch &&|| click
        Vector3 direction;
        
        direction = new Vector3(
        TouchInput.getTouch().x - turningAxis.transform.position.x,
        TouchInput.getTouch().y - turningAxis.transform.position.y,0);

        
        //Turn the guns axis in the right direction
        turningAxis.transform.right = direction;

        //Resatrain gun angle
        turningAxis.transform.rotation = Quaternion.Euler(0,0, Mathf.Clamp(turningAxis.transform.eulerAngles.z, 80, 180));
        
        //On click || pres, & if alowed by firecountown call Shoot(), to execute fireing
        if (TouchInput.Shoot() && firecountdown >= 1f)
        {
            //Change the value to 1 to represent "per second"
            firecountdown = 0f;
            Shoot();
            
        }

        // fire rate of 0,5 would represent 0,5 shots per second, fire countdown previously set to 1
        firecountdown += Time.deltaTime * fireRate;
    }

    void Shoot()
    {
        if (TouchInput.getTouch().y > 1.5f)
            explosionHeightY = TouchInput.getTouch().y;
        else
            explosionHeightY = 4f;
        //Spawns the object from the pool
            objectPooling.SpawnFromPool(missile.name, barrel.transform.position, barrel.transform.rotation);
    }

}
