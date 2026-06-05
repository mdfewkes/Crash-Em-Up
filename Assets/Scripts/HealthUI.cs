using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image hpFillImage;
    [SerializeField] private Image spinFillImage;

    private void OnEnable()
    {
        if (health == null)
            health = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<Health>();
        health.OnDamage += Health_OnDamage;
    }

    private void Start()
    {
        spinFillImage.fillAmount = 1;
        hpFillImage.fillAmount = 1;
    }

	private void Update()
	{
		spinFillImage.fillAmount =  health.GetCurrentSpinout() /   health.GetStartingSpinout();
	}

	private void OnDisable()
    {
        health.OnDamage -= Health_OnDamage;
    }

    private void Health_OnDamage()
    {
        hpFillImage.fillAmount =  health.GetCurrentHealth() /   health.GetStartingHealth();

    }
}
