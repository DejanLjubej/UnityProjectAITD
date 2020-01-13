

public class HealthSystem{
    float health;

    public HealthSystem(float health)
    {
        this.health = health;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
    }
}
