using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;

    private void OnEnable()
    {
        if (health == null)
            health = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<Health>();
        health.OnDamage += Health_OnDamage;
    }

    private void Start()
    {
       

        fillImage.fillAmount = 1;
    }

    private void OnDisable()
    {
        health.OnDamage -= Health_OnDamage;
    }

    private void Health_OnDamage()
    {
        fillImage.fillAmount =  health.GetCurrentHealth() /   health.GetStartingHealth();

        
    }
}
