using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {


    static bool shoot = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        getTouch();

        if (Input.GetMouseButton(0))
        {
            shoot = true;
            Shoot();
        }
        else
        {
            shoot = false;
           
        }
    }

    public static Vector3 getTouch()
    {
        Vector3 mousePosition = Input.mousePosition;
        return mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        /*
        Vector2 direction = new Vector2()
        Debug.Log(Input.mousePosition);
        return Input.mousePosition;
        */
    }

    public static bool Shoot()
    {
        return shoot;
    }
}
