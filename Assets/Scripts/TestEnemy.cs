using UnityEngine;

public class TestEnemy : EnemyBase {
	[SerializeField] private EnemyAction uAttack;
	[SerializeField] private EnemyAction dAttack;

	protected override void MoveState() {
		base.MoveState();

		FindAction();
	}

	private void FindAction() {
		if (PlayerTag.playerTag == null) return;

		Vector2 direction = new Vector2(PlayerTag.playerTag.transform.position.x - transform.position.x, PlayerTag.playerTag.transform.position.z - transform.position.z).normalized;
		float distance = Vector3.Distance(transform.position, PlayerTag.playerTag.transform.position);
		float upProduct = Vector2.Dot(Vector2.up, direction);

		if (distance < 3.0f) {
			if (upProduct > 0.7f) RequestStartActionState(uAttack);
			else if (upProduct < -0.7f) RequestStartActionState(dAttack);
		}
	}

}
