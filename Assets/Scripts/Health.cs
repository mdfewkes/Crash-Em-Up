using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Health : MonoBehaviour  {
    public event System.Action<Health> OnDied;
    public static event System.Action<int> OnScoreUpdate;
    public event Action OnDamage;

    private bool isDead = false;
    private bool isLastHit = false;

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

    public bool IsLastHit()
    {
        return isLastHit;
    }

    public void TakeDamage(float collisionDamage, float spinoutDamage) {
        if (invincible) return;

        collisionHP -= collisionDamage;
        spinoutHP -= spinoutDamage;
        Mathf.Max(0.0f, collisionHP);

        if (collisionHP - collisionDamage <= 0)
            isLastHit = true;

        OnDamage?.Invoke();

        if(spinoutHP <= 0) {
            collisionHP += spinoutHP;
        }

        if(collisionHP <= 0 && !isDead) {
            isDead = true;
            if (gameObject.CompareTag("Enemy"))
            {
                int scoreAmt = UnityEngine.Random.Range(100, 200 + 1);
                OnScoreUpdate?.Invoke(scoreAmt);
            }
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