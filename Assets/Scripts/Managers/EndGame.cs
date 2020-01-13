using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    private bool gameEnded = false;

    public GameObject goUI;
    public GameObject gun;

    void Start()
    {

    }
    void Update()
    {

        if (gameEnded)
            return;

        if(gun.activeSelf == false)
        {
            EndIt();
        }
    }

    void EndIt()
    {
        gameEnded = true;
        goUI.SetActive(true);

    }
}
