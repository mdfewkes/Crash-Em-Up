using UnityEngine;

public class TestEnemy : EnemyBase {
	[SerializeField] private EnemyAction uAttack;
	[SerializeField] private EnemyAction dAttack;
	
	private PlayerController player;

	new void Start() {
		base.Start();

		player = FindAnyObjectByType<PlayerController>();
	}

	protected override void MoveState() {
		base.MoveState();

		FindAction();
	}

	private void FindAction() {
		if (player == null) return;

		Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z).normalized;
		float distance = Vector3.Distance(transform.position, player.transform.position);
		float upProduct = Vector2.Dot(Vector2.up, direction);

		if (distance < 3.0f) {
			if (upProduct > 0.7f) RequestStartActionState(uAttack);
			else if (upProduct < -0.7f) RequestStartActionState(dAttack);
		}
	}

}
