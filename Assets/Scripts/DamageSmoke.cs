using UnityEngine;

public class DamageSmoke : MonoBehaviour
{
    [SerializeField] private GameObject smokeDamagedPrefab;
    [SerializeField] private GameObject smokeNearlyDestroyedPrefab;

    private GameObject smokeDamaged;
    private GameObject smokeNearlyDestroyed;

    [SerializeField] private Transform spawnPonint;
    [SerializeField] private Health health;

    private void OnEnable()
    {
        if(health is null)
        health = GetComponentInParent<Health>();

        health.OnDamage += Health_OnDamage;
    }

    private void OnDisable()
    {
        health.OnDamage -= Health_OnDamage;
    }

    private void Health_OnDamage()
    {
        if (health.GetCurrentHealth()/health.GetStartingHealth() <=0.6f && smokeDamaged is null)
        {
           smokeDamaged = Instantiate(smokeDamagedPrefab,spawnPonint.position,Quaternion.Euler(-90,0,0),transform);
        }
        else if (health.IsLastHit() && smokeNearlyDestroyed is null)
        {
            smokeNearlyDestroyed = Instantiate(smokeNearlyDestroyedPrefab, spawnPonint.position, Quaternion.Euler(-90, 0, 0), transform);
            smokeDamaged.SetActive(false);
        }
    }
}
