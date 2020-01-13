using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldiers : MonoBehaviour {

    public float spawnTimer = 5f;
    public  float Timer;

    public GameObject spawnPoint;
    public GameObject soldier;

    ObjectPooling objectPooling;

	// Use this for initialization
	void Start () {
        objectPooling = ObjectPooling.Instance;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Timer+= Time.deltaTime;
        if(Timer>= spawnTimer)
        {
            Spawn();
            Timer = 0;
        }
	}

    void Spawn()
    {
        if(soldier != null)
        {
            Debug.LogError(soldier.name + " Eys Er dRzrtrrrr!!");
            objectPooling.SpawnFromPool(soldier.name, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {
            Debug.LogError("Yi geEt nathn op'm sldrz");
        }
        
    }
}
