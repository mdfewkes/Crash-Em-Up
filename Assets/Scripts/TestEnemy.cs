using UnityEngine;

public class TestEnemy : CharacterBase {
	[SerializeField] float speed = 3.0f;

	Vector3 startingPosition;

	protected override void OnStart() {
		startingPosition = transform.position;
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
