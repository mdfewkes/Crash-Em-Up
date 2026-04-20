using UnityEngine;

public class TestEnemy : CharacterBase {
	[SerializeField] private Health health;

	SteeringComponent steeringComponent;


	protected override void OnStart() {
		steeringComponent = GetComponent<SteeringComponent>();
		steeringComponent.SetTarget(transform.position);
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
		Vector3 newPosition = transform.position;
		Vector2 steeringVector = steeringComponent.UpdateSteeringVector();
		newPosition.x += steeringVector.x;
		newPosition.z += steeringVector.y;
		transform.position = newPosition;
	}
}
