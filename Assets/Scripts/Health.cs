using System;
using UnityEngine;

public class Health : MonoBehaviour  {
    public event System.Action<Health> OnDied;
    public event Action OnDamage;

    private bool isDead = false;

	[SerializeField]
	private bool invincible = false;
	[SerializeField]
	private float startingCrashHealth;
	[SerializeField]
	private float startingSpinoutHealth;
	private float collisionHP = 10.0f; 
	private float spinoutHP = 10.0f; 

    void Start() {
        collisionHP = startingCrashHealth;
        spinoutHP = startingSpinoutHealth;
    }

    void Update() {
        if (spinoutHP < startingSpinoutHealth) {
            spinoutHP += Time.deltaTime;
        }
    }

    public void TakeDamage(float collisionDamage, float spinoutDamage) {
        if (invincible) return;

        collisionHP -= collisionDamage;
        spinoutHP -= spinoutDamage;
        Mathf.Max(0.0f, collisionHP);
        OnDamage?.Invoke();

        if(spinoutHP <= 0) {
            collisionHP += spinoutHP;
        }

        if(collisionHP <= 0 && !isDead) {
            isDead = true;
           Die();
        }
    }

    public float GetCurrentHealth() => collisionHP;
    public float GetStartingHealth() => startingCrashHealth;

    private void Die() {
        OnDied?.Invoke(this);
        Destroy(gameObject);
    }

}