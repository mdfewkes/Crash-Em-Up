using UnityEngine;

public class Health : MonoBehaviour 
{
    public event System.Action<Health> OnDied;

    private bool isDead = false;

	[SerializeField]
	private bool invincible = false;
	[SerializeField]
	private float startingHealth;
	private float health = 10.0f; 

    void Start() 
    {
        health = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        if (invincible) return;

        health -= damage;
        Debug.Log("Damage Done: " + damage);
        Mathf.Max(0.0f,health);

        if(health <= 0 && !isDead)
        {
            isDead = true;
           Die();
        }
    }

    private void Die()
    {
        OnDied?.Invoke(this);
        Destroy(gameObject);
    }

}