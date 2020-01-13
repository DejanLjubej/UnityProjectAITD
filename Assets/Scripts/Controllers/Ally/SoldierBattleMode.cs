using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBattleMode : MonoBehaviour {

    public float demolishRange = 1f;
    public float shootRange = 4f;
    public int demolishDamage = 5;
    public int shootDamage = 1;

    public float shootingRate;
    public float demolishRate;
    public float demolishTimer = 5f;
    public float shootTimer = 1f;



    public GameObject shotAnimation;

    ObjectPooling objectPooling;
    // Use this for initialization
    void Start()
    {
        objectPooling = ObjectPooling.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        shootingRate += Time.deltaTime;
        demolishRate += Time.deltaTime;

        if(demolishRate >= demolishTimer)
        {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, demolishRange);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Loot")
                {
                    DemolishDamage(collider.transform);
                }
            }
        }
        if (shootingRate >= shootTimer)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, shootRange);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    PlayerDamage(collider.transform);
                }
            }
        }
    }

    void DemolishDamage(Transform loot)
    {
        demolishRate = 0;
        PlaneStats uS = loot.GetComponent<PlaneStats>();
        if (uS != null)
            uS.TakeDamage(demolishDamage);
        ScoreInScene.lootGathered +=demolishDamage;
    }

    void PlayerDamage(Transform player)
    {
        if (shotAnimation != null)
        {
            GameObject go = objectPooling.SpawnFromPool(shotAnimation.name, transform.position, transform.rotation);
            go.SetActive(true);
        }

        shootingRate = 0;
        PlaneStats uS = player.GetComponent<PlaneStats>();
        if (uS != null)
            uS.TakeDamage(shootDamage);
    }

}
