using UnityEngine;

public class TestEnemy : CharacterBase {
	[SerializeField] float speed = 3.0f;
	[SerializeField] private Health health;

	Vector3 startingPosition;

	protected override void OnStart() {
		startingPosition = transform.position;
		health.OnDied += Health_OnDied;

    }

    public static event System.Action OnEnemyDestroyed;


    private void OnDestroy()
    {
        health.OnDied -= Health_OnDied;
    }

    private void Health_OnDied(Health obj)
    {
        if (obj == this.GetComponent<Health>())
        {
            Debug.Log("enemy destoryed");
            OnEnemyDestroyed?.Invoke();
        }
    }

	protected override void MoveState() {
		Vector3 pointToStart = startingPosition - transform.position;
		if (pointToStart.magnitude > speed) {
			pointToStart = pointToStart.normalized * speed;
		}
		Vector2 goHome = new Vector2(pointToStart.x, pointToStart.z) * Time.deltaTime;

		Vector3 newPosition = transform.position;
		newPosition.x += goHome.x;
		newPosition.z += goHome.y;
		transform.position = newPosition;
	}
}
