using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float startingHealth;
    private float health;    

    void Start() 
    {
        health = startingHealth;
    }

    public void TakeDamage(float damage)
    {
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