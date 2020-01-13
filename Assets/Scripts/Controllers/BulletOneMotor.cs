using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOneMotor : MonoBehaviour {

    public float damage = 100;

    public float speed=5;
    public float explosionRadius = 0f;
    public float explosionHeight = 4f;
    public float bulletLife=1;
    public float bulletLifetime;

    public int pushAmount = 1;
    public int amoutOfTimesPushed = 0;
    public bool enemyAmmo = false;

    public Vector2 startingPosition;
    public Vector2 currentPosition;

    public ParticleSystem explosion;

    Rigidbody2D bulletRb;
    Vector3 lastPosition;

    //Scripts
    TurretAiming turretAiming;
    ObjectPooling objectPooling;
    PlaneStats planeStats;


    // Use this for initialization
    void Start () {
        objectPooling = ObjectPooling.Instance;
        bulletRb = GetComponent<Rigidbody2D>();
        lastPosition = currentPosition;
        
        startingPosition = transform.position;

        bulletRb.velocity = transform.right * speed;
        amoutOfTimesPushed++;
 
    }

    public void AddVelocity()
    {
        lastPosition = currentPosition;
        bulletRb.velocity = transform.right * speed;
    }
    // Update is called once per frame
    void FixedUpdate () {

        if (this.gameObject.activeSelf && pushAmount>amoutOfTimesPushed)
        {
            
            AddVelocity();
            amoutOfTimesPushed++;

        }
        explosionHeight = TurretAiming.YAxisOnClick()+0.2f;


        currentPosition = transform.position;

        Vector2 magnitude = startingPosition + currentPosition;


        bulletLifetime += Time.deltaTime;
        if(bulletLifetime >= bulletLife)
        {
            amoutOfTimesPushed = 0;
            this.gameObject.SetActive(false);
            bulletLifetime = 0;
        }
        if( enemyAmmo != true)
        {
            if (currentPosition.y >= explosionHeight )
            {
                ExplosionAnimation();
                if (explosionRadius > 0)
                {
                    Explode();
                }
                amoutOfTimesPushed = 0;
                this.gameObject.SetActive(false);
                bulletLifetime = 0;
            }
        }

        Vector3 direction = transform.position - lastPosition;
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        //this.transform.rotation = Quaternion.Euler(0,0,direction.z);
        this.transform.right = -direction;

        lastPosition = transform.position;

	}

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyAmmo != true)
        {
            if (explosionRadius > 0f)
            {
                DirectDamage(collision.transform);
                Explode();
            }
            else
            {
                DirectDamage(collision.transform);
            }

            ExplosionAnimation();
            this.gameObject.SetActive(false);
            bulletLifetime = 0;
            amoutOfTimesPushed = 0;

        }else if(collision.tag != "Enemy" && enemyAmmo == true)
        {
            if (explosionRadius > 0f)
            {
                DirectDamage(collision.transform);
                Explode();
            }
            else
            {
                DirectDamage(collision.transform);
            }

            ExplosionAnimation();
            Explode();
            this.gameObject.SetActive(false);
            bulletLifetime = 0;
            amoutOfTimesPushed = 0;
        
        }
       
        return;
    }

    void Explode()
    {
        //Debug.Log(transform.position.y);
        Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {

            if(collider.tag == "Enemy")
            {
            }
                ExplosionDamage(collider.transform);
        }
    }
    void ExplosionDamage(Transform enemy)
    {
        if (enemy.tag == "Enemy")
        {
        }
            PlaneStats ps = enemy.GetComponent<PlaneStats>();
        if(ps != null)
        {
            Vector3 psr = ps.transform.position;

            float distanceFromBulletToObj = Vector3.Distance(ps.transform.position, this.transform.position);
            float damageDoneBasedOnRange = 1 - distanceFromBulletToObj / explosionRadius;

            if (damageDoneBasedOnRange > 0)
            {
                ps.TakeDamage(damage * damageDoneBasedOnRange);
            }

        }


    }

    void DirectDamage(Transform enemy)
    {
        if(enemy.tag == "Enemy")
        {
        }
            PlaneStats ps = enemy.GetComponent<PlaneStats>();
        if(ps != null)
        {

            ps.TakeDamage(damage);
        }
    }

    void ExplosionAnimation()
    {
        objectPooling.SpawnFromPool(explosion.name, transform.position, transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
