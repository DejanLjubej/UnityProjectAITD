using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Rigidbody2D[] planes;

    public float timeBetweenSpawns = 1f;
    public float spawnUp = 2;
    public float spawnDown = -1;

    float timer;

    ObjectPooling objectPooling;
	// Use this for initialization
	void Start () {
        objectPooling = ObjectPooling.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
       

        if (timer >= timeBetweenSpawns)
        {
            SpawnPlane();
            timer = 0;
        }
    }

    void SpawnPlane()
    {
        float a=planes.Length;
        //Rigidbody2D sprite = (Rigidbody2D) Instantiate(planes[Random.Range(0,planes.Length)], new Vector2(transform.position.x, transform.position.y+Random.Range(spawnDown, spawnUp)), transform.rotation);
        objectPooling.SpawnFromPool(planes[Random.Range(0, planes.Length)].name, new Vector2(transform.position.x, transform.position.y + Random.Range(spawnDown, spawnUp)), transform.rotation);
    } 
}
