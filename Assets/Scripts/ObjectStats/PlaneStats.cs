using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneStats : MonoBehaviour {

    public float lifetime = 0;
    float timeAlive;

    public float health=100;
    public int pointsWorth;
    HealthSystem healthSystem;

    public GameObject debree;

    public Image helthbar;

    public float currentHealth;

    ObjectPooling objectPooling;

    private void Awake()
    {
        resetHealth();
    }
    void resetHealth()
    {
        healthSystem = new HealthSystem(health);
        objectPooling = ObjectPooling.Instance;

    }
    void Start()
    {
        
    }

    void Score()
    {
        switch (this.gameObject.tag)
        {
            case "Enemy":
                ScoreInScene.enemiesDestroyed += pointsWorth;
                break;
            default:
                break;
        }
    }
    // function called from damage source to deal damage to the object
    public void TakeDamage(float amount)
    {

        //Deal the amount of damage to this object
        healthSystem.Damage(amount);

        //When health reaches 0 remove this object
        if(healthSystem.GetHealth() <= 0)
        {
            Remove();
        }
        if(helthbar != null)
        {
            helthbar.fillAmount = healthSystem.GetHealth()/health;
        }
    }

    void Remove()
    {
        ScoreInScene.enemiesDestroyed += pointsWorth;
        
        this.gameObject.SetActive(false);
        resetHealth();
        
        //Destroy(this.gameObject);
        if (debree != null)
        {
            //GameObject loot = (GameObject)Instantiate(debree, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            objectPooling.SpawnFromPool(debree.name, transform.position, transform.rotation);
            Debug.Log("Am I doing anything at all? or why is there no loot");
        }
        Score();
    }

    void FixedUpdate()
    {
        currentHealth = healthSystem.GetHealth();
        timeAlive += Time.deltaTime;

        if (this.transform.position.x > 12.5f)
        {
            Remove();
        }

        if (Input.GetKeyDown("space"))
        {
        }

        if (Input.GetKeyDown("h"))
        {
            healthSystem.Heal(10);
            Debug.Log("Ham");
            Debug.Log("What's my health now? It's " + healthSystem.GetHealth());
        }
    }   

}
