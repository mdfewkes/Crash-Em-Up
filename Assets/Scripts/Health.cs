using UnityEngine;

public class Health : MonoBehaviour 
{

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
        if (invincible)  return;

        health -= damage;
        Mathf.Max(0.0f,health);

        if(health <= 0)
        {
           Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}