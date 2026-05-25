using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    [SerializeField] private float timeInSeconds = 1.0f;
    private float timeRemaining;

	void Start() {
		timeRemaining = timeInSeconds;
	}

	void Update() {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f) Destroy(gameObject);
    }
}
